using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class CategoryRepositoryTest : RepositoryTest
    {
        private ICategoryRepository Repository { get; set; }

        private int UserId { get; set; } = 1;

        [TestInitialize]
        public void Setup()
        {
            Repository = new CategoryRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetCategories()
        {
            var categories = await Repository.GetCategories(UserId);
            Assert.IsNotNull(categories);
            Assert.AreNotEqual(categories.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetCategoryById()
        {
            var categories = await Repository.GetCategories(UserId);
            Assert.IsNotNull(categories);
            Assert.AreNotEqual(categories.Count, 0);

            var id = categories.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var account = await Repository.GetCategoryById(id.Value);
                Assert.IsNotNull(account);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateCategory()
        {
            var id = await Repository.CreateCategory(UserId, "testName", "testDescription");
            Assert.AreNotEqual(id, 0);

            var category = await Repository.GetCategoryById(id);
            Assert.IsNotNull(category);
            Assert.AreEqual(category.Name, "testName");
            Assert.AreEqual(category.Description, "testDescription");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateCategory()
        {
            var categories = await Repository.GetCategories(UserId);
            Assert.IsNotNull(categories);
            Assert.AreNotEqual(categories.Count, 0);

            var category = categories.FirstOrDefault(i => i.Name.Equals("testName") && i.Description.Equals("testDescription"));
            Assert.IsNotNull(category);

            var updated = await Repository.UpdateCategory(category.ID, "testName2", "testDescription2");
            Assert.IsTrue(updated);

            category = await Repository.GetCategoryById(category.ID);
            Assert.IsNotNull(category);
            Assert.AreEqual(category.Name, "testName2");
            Assert.AreEqual(category.Description, "testDescription2");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteCategory()
        {
            var categories = await Repository.GetCategories(UserId);
            Assert.IsNotNull(categories);
            Assert.AreNotEqual(categories.Count, 0);

            var category = categories.FirstOrDefault(i => i.Name.Equals("testName2") && i.Description.Equals("testDescription2"));
            Assert.IsNotNull(category);

            var deleted = await Repository.DeleteCategory(category.ID);
            Assert.IsTrue(deleted);

            category = await Repository.GetCategoryById(category.ID);
            Assert.IsNull(category);
        }

        [TestCleanup]
        public void Clean()
        {

        }
    }
}