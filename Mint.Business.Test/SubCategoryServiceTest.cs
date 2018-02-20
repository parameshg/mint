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
    public class SubCategorieserviceTest : ServiceTest
    {
        private Mock<ISubCategoryRepository> Repository { get; set; }

        private ISubCategoryService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var category = new SubCategory() { ID = 2, Name = "Rent", Description = "Home Rent" };

            Repository = new Mock<ISubCategoryRepository>();

            Repository.Setup(i => i.GetSubCategories(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SubCategory>(new SubCategory[] { category }));

            Repository.Setup(i => i.GetSubCategoryById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(category);

            Repository.Setup(i => i.CreateSubCategory(
                It.IsAny<int>(), // user
                It.IsAny<int>(), // category
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateSubCategory(
                It.IsAny<int>(), // id
                It.IsAny<int>(), // category
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteSubCategory(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            Service = new SubCategoryService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetSubCategories()
        {
            var categories = await Service.GetSubCategories(new Random().Next());

            Assert.IsNotNull(categories);
            Assert.AreEqual(categories.Count, 1);

            Repository.Verify(i => i.GetSubCategories(It.Is<int>(o => o.Equals(1024)), It.IsAny<int>()), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetSubCategoryById()
        {
            var category = await Service.GetSubCategoryById(2);

            Assert.IsNotNull(category);

            Repository.Verify(i => i.GetSubCategoryById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateSubCategory()
        {
            var created = await Service.CreateSubCategory(new Random().Next(), "Train", "Travel by Train");

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateSubCategory(
                It.Is<int>(user => user.Equals(1024)),
                It.IsAny<int>(),
                It.Is<string>(name => name.Equals("Train")),
                It.Is<string>(description => description.Equals("Travel by Train"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateSubCategory()
        {
            var updated = await Service.UpdateSubCategory(2, new Random().Next(), "Bus", "Travel by Bus");

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateSubCategory(
                It.Is<int>(id => id.Equals(2)),
                It.IsAny<int>(),
                It.Is<string>(lastName => lastName.Equals("Bus")),
                It.Is<string>(phone => phone.Equals("Travel by Bus"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteSubCategory()
        {
            var deleted = await Service.DeleteSubCategory(2, 3);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteSubCategory(It.Is<int>(id => id.Equals(2)), It.Is<int>(id => id.Equals(3))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}