using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class TagRepositoryTest : RepositoryTest
    {
        private ITagRepository Repository { get; set; }

        private int UserId { get; set; } = 1;

        [TestInitialize]
        public void Setup()
        {
            Repository = new TagRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetTags()
        {
            var Tags = await Repository.GetTags(UserId);
            Assert.IsNotNull(Tags);
            Assert.AreNotEqual(Tags.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetTagById()
        {
            var Tags = await Repository.GetTags(UserId);
            Assert.IsNotNull(Tags);
            Assert.AreNotEqual(Tags.Count, 0);

            var id = Tags.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var Tag = await Repository.GetTagById(id.Value);
                Assert.IsNotNull(Tag);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateTag()
        {
            var id = await Repository.CreateTag(UserId, "testName", "testDescription");
            Assert.AreNotEqual(id, 0);

            var Tag = await Repository.GetTagById(id);
            Assert.IsNotNull(Tag);
            Assert.AreEqual(Tag.Name, "testName");
            Assert.AreEqual(Tag.Description, "testDescription");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateTag()
        {
            var Tags = await Repository.GetTags(UserId);
            Assert.IsNotNull(Tags);
            Assert.AreNotEqual(Tags.Count, 0);

            var Tag = Tags.FirstOrDefault(i => i.Name.Equals("testName") && i.Description.Equals("testDescription"));
            Assert.IsNotNull(Tag);

            var updated = await Repository.UpdateTag(Tag.ID, "testName2", "testDescription2");
            Assert.IsTrue(updated);

            Tag = await Repository.GetTagById(Tag.ID);
            Assert.IsNotNull(Tag);
            Assert.AreEqual(Tag.Name, "testName2");
            Assert.AreEqual(Tag.Description, "testDescription2");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteTag()
        {
            var Tags = await Repository.GetTags(UserId);
            Assert.IsNotNull(Tags);
            Assert.AreNotEqual(Tags.Count, 0);

            var Tag = Tags.FirstOrDefault(i => i.Name.Equals("testName2") && i.Description.Equals("testDescription2"));
            Assert.IsNotNull(Tag);

            var deleted = await Repository.DeleteTag(Tag.ID);
            Assert.IsTrue(deleted);

            Tag = await Repository.GetTagById(Tag.ID);
            Assert.IsNull(Tag);
        }

        [TestCleanup]
        public void Clean()
        {

        }
    }
}