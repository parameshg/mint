using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class SubCategoryRepositoryTest : RepositoryTest
    {
        private ISubCategoryRepository Repository { get; set; }

        private int UserId { get; set; } = 1;

        private int CategoryId { get; set; } = 2;

        [TestInitialize]
        public void Setup()
        {
            Repository = new SubCategoryRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetsubcategories()
        {
            var subCategories = await Repository.GetSubCategories(UserId, CategoryId);
            Assert.IsNotNull(subCategories);
            Assert.AreNotEqual(subCategories.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetSubCategoryById()
        {
            var subCategories = await Repository.GetSubCategories(UserId, CategoryId);
            Assert.IsNotNull(subCategories);
            Assert.AreNotEqual(subCategories.Count, 0);

            var id = subCategories.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var subCategory = await Repository.GetSubCategoryById(id.Value);
                Assert.IsNotNull(subCategory);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateSubCategory()
        {
            var id = await Repository.CreateSubCategory(UserId, CategoryId, "testName", "testDescription");
            Assert.AreNotEqual(id, 0);

            var SubCategory = await Repository.GetSubCategoryById(id);
            Assert.IsNotNull(SubCategory);
            Assert.AreEqual(SubCategory.Name, "testName");
            Assert.AreEqual(SubCategory.Description, "testDescription");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateSubCategory()
        {
            var subCategories = await Repository.GetSubCategories(UserId, CategoryId);
            Assert.IsNotNull(subCategories);
            Assert.AreNotEqual(subCategories.Count, 0);

            var subCategory = subCategories.FirstOrDefault(i => i.Name.Equals("testName") && i.Description.Equals("testDescription"));
            Assert.IsNotNull(subCategory);

            var updated = await Repository.UpdateSubCategory(subCategory.ID, CategoryId, "testName2", "testDescription2");
            Assert.IsTrue(updated);

            subCategory = await Repository.GetSubCategoryById(subCategory.ID);
            Assert.IsNotNull(subCategory);
            Assert.AreEqual(subCategory.Name, "testName2");
            Assert.AreEqual(subCategory.Description, "testDescription2");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteSubCategory()
        {
            var subCategories = await Repository.GetSubCategories(UserId, CategoryId);
            Assert.IsNotNull(subCategories);
            Assert.AreNotEqual(subCategories.Count, 0);

            var subCategory = subCategories.FirstOrDefault(i => i.Name.Equals("testName2") && i.Description.Equals("testDescription2"));
            Assert.IsNotNull(subCategory);

            var deleted = await Repository.DeleteSubCategory(CategoryId, subCategory.ID);
            Assert.IsTrue(deleted);

            subCategory = await Repository.GetSubCategoryById(subCategory.ID);
            Assert.IsNull(subCategory);
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}