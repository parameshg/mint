using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Business.Services;
using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Test
{
    [TestClass]
    public class CategorieserviceTest : ServiceTest
    {
        private Mock<ICategoryRepository> Repository { get; set; }

        private ICategoryService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var Category = new Category() { ID = 2, Name = "Home", Description = "Home" };

            Repository = new Mock<ICategoryRepository>();

            Repository.Setup(i => i.GetCategories(It.IsAny<int>())).ReturnsAsync(new List<Category>(new Category[] { Category }));

            Repository.Setup(i => i.GetCategoryById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(Category);

            Repository.Setup(i => i.CreateCategory(
                It.IsAny<int>(), // user
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateCategory(
                It.IsAny<int>(), // id
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteCategory(It.IsAny<int>())).ReturnsAsync(true);

            Service = new CategoryService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetCategories()
        {
            var categories = await Service.GetCategories();

            Assert.IsNotNull(categories);
            Assert.AreEqual(categories.Count, 1);

            Repository.Verify(i => i.GetCategories(It.Is<int>(o => o.Equals(1024))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetCategoryById()
        {
            var category = await Service.GetCategoryById(2);

            Assert.IsNotNull(category);

            Repository.Verify(i => i.GetCategoryById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateCategory()
        {
            var created = await Service.CreateCategory("Travel", "Travel Category");

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateCategory(
                It.Is<int>(user => user.Equals(1024)),
                It.Is<string>(name => name.Equals("Travel")),
                It.Is<string>(description => description.Equals("Travel Category"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateCategory()
        {
            var updated = await Service.UpdateCategory(2, "Entertainment", "Entertainment Category");

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateCategory(
                It.Is<int>(id => id.Equals(2)),
                It.Is<string>(name => name.Equals("Entertainment")),
                It.Is<string>(description => description.Equals("Entertainment Category"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteCategory()
        {
            var deleted = await Service.DeleteCategory(2);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteCategory(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}