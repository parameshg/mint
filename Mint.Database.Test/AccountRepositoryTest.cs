using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class AccountRepositoryTest : RepositoryTest
    {
        private IAccountRepository Repository { get; set; }

        private int UserId { get; set; } = 1;

        [TestInitialize]
        public void Setup()
        {
            Repository = new AccountRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetAccounts()
        {
            var accounts = await Repository.GetAccounts(UserId);
            Assert.IsNotNull(accounts);
            Assert.AreNotEqual(accounts.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetAccountById()
        {
            var accounts = await Repository.GetAccounts(UserId);
            Assert.IsNotNull(accounts);
            Assert.AreNotEqual(accounts.Count, 0);

            var id = accounts.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var account = await Repository.GetAccountById(id.Value);
                Assert.IsNotNull(account);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateAccount()
        {
            var id = await Repository.CreateAccount(UserId, "testName", "testDescription");
            Assert.AreNotEqual(id, 0);

            var account = await Repository.GetAccountById(id);
            Assert.IsNotNull(account);
            Assert.AreEqual(account.Name, "testName");
            Assert.AreEqual(account.Description, "testDescription");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateAccount()
        {
            var accounts = await Repository.GetAccounts(UserId);
            Assert.IsNotNull(accounts);
            Assert.AreNotEqual(accounts.Count, 0);

            var account = accounts.FirstOrDefault(i => i.Name.Equals("testName") && i.Description.Equals("testDescription"));
            Assert.IsNotNull(account);

            var updated = await Repository.UpdateAccount(account.ID, "testName2", "testDescription2");
            Assert.IsTrue(updated);

            account = await Repository.GetAccountById(account.ID);
            Assert.IsNotNull(account);
            Assert.AreEqual(account.Name, "testName2");
            Assert.AreEqual(account.Description, "testDescription2");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteAccount()
        {
            var accounts = await Repository.GetAccounts(UserId);
            Assert.IsNotNull(accounts);
            Assert.AreNotEqual(accounts.Count, 0);

            var account = accounts.FirstOrDefault(i => i.Name.Equals("testName2") && i.Description.Equals("testDescription2"));
            Assert.IsNotNull(account);

            var deleted = await Repository.DeleteAccount(account.ID);
            Assert.IsTrue(deleted);

            account = await Repository.GetAccountById(account.ID);
            Assert.IsNull(account);
        }

        [TestCleanup]
        public void Clean()
        {

        }
    }
}