using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class TransactionTypeRepositoryTest : RepositoryTest
    {
        private ITransactionTypeRepository Repository { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Repository = new TransactionTypeRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetTransactionTypes()
        {
            var types = await Repository.GetTransactionTypes();
            Assert.IsNotNull(types);
            Assert.AreNotEqual(types.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetTransactionTypeById()
        {
            var types = await Repository.GetTransactionTypes();
            Assert.IsNotNull(types);
            Assert.AreNotEqual(types.Count, 0);

            var id = types.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var type = await Repository.GetTransactionTypeById(id.Value);
                Assert.IsNotNull(type);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateTransactionType()
        {
            var id = await Repository.CreateTransactionType("testName", "testDescription");
            Assert.AreNotEqual(id, 0);

            var type = await Repository.GetTransactionTypeById(id);
            Assert.IsNotNull(type);
            Assert.AreEqual(type.Name, "testName");
            Assert.AreEqual(type.Description, "testDescription");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateTransactionType()
        {
            var types = await Repository.GetTransactionTypes();
            Assert.IsNotNull(types);
            Assert.AreNotEqual(types.Count, 0);

            var type = types.FirstOrDefault(i => i.Name.Equals("testName") && i.Description.Equals("testDescription"));
            Assert.IsNotNull(type);

            var updated = await Repository.UpdateTransactionType(type.ID, "testName2", "testDescription2");
            Assert.IsTrue(updated);

            type = await Repository.GetTransactionTypeById(type.ID);
            Assert.IsNotNull(type);
            Assert.AreEqual(type.Name, "testName2");
            Assert.AreEqual(type.Description, "testDescription2");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteTransactionType()
        {
            var types = await Repository.GetTransactionTypes();
            Assert.IsNotNull(types);
            Assert.AreNotEqual(types.Count, 0);

            var type = types.FirstOrDefault(i => i.Name.Equals("testName2") && i.Description.Equals("testDescription2"));
            Assert.IsNotNull(type);

            var deleted = await Repository.DeleteTransactionType(type.ID);
            Assert.IsTrue(deleted);

            type = await Repository.GetTransactionTypeById(type.ID);
            Assert.IsNull(type);
        }

        [TestCleanup]
        public void Clean()
        {

        }
    }
}