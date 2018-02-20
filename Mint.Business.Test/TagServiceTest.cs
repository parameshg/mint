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
    public class TagServiceTest : ServiceTest
    {
        private Mock<ITagRepository> Repository { get; set; }

        private ITagService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var tag = new Tag() { ID = 2, Name = "Investments", Description = "Long-Term Investments" };

            Repository = new Mock<ITagRepository>();

            Repository.Setup(i => i.GetTags(It.IsAny<int>())).ReturnsAsync(new List<Tag>(new Tag[] { tag }));

            Repository.Setup(i => i.GetTagById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(tag);

            Repository.Setup(i => i.CreateTag(
                It.IsAny<int>(), // user
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateTag(
                It.IsAny<int>(), // id
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteTag(It.IsAny<int>())).ReturnsAsync(true);

            Service = new TagService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetTags()
        {
            var tags = await Service.GetTags();

            Assert.IsNotNull(tags);
            Assert.AreEqual(tags.Count, 1);

            Repository.Verify(i => i.GetTags(It.Is<int>(o => o.Equals(1024))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetTagById()
        {
            var tag = await Service.GetTagById(2);

            Assert.IsNotNull(tag);

            Repository.Verify(i => i.GetTagById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateTag()
        {
            var created = await Service.CreateTag("Capital", "Capital Expenses");

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateTag(
                It.Is<int>(user => user.Equals(1024)),
                It.Is<string>(name => name.Equals("Capital")),
                It.Is<string>(description => description.Equals("Capital Expenses"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateTag()
        {
            var updated = await Service.UpdateTag(2, "Assets", "Material Assets");

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateTag(
                It.Is<int>(id => id.Equals(2)),
                It.Is<string>(name => name.Equals("Assets")),
                It.Is<string>(description => description.Equals("Material Assets"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteTag()
        {
            var deleted = await Service.DeleteTag(2);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteTag(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}