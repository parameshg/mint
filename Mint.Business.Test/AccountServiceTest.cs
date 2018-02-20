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
    public class AccountServiceTest : ServiceTest
    {
        private Mock<IAccountRepository> Repository { get; set; }

        private IAccountService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var account = new Account() { ID = 2, Name = "Savings", Description = "Savings Account" };

            Repository = new Mock<IAccountRepository>();

            Repository.Setup(i => i.GetAccounts(It.IsAny<int>())).ReturnsAsync(new List<Account>(new Account[] { account }));

            Repository.Setup(i => i.GetAccountById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(account);

            Repository.Setup(i => i.CreateAccount(
                It.IsAny<int>(), // user
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateAccount(
                It.IsAny<int>(), // id
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteAccount(It.IsAny<int>())).ReturnsAsync(true);

            Service = new AccountService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetAccounts()
        {
            var users = await Service.GetAccounts();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 1);

            Repository.Verify(i => i.GetAccounts(It.Is<int>(o => o.Equals(1024))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetAccountById()
        {
            var account = await Service.GetAccountById(2);

            Assert.IsNotNull(account);

            Repository.Verify(i => i.GetAccountById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateAccount()
        {
            var created = await Service.CreateAccount("Checking", "Checking Account");

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateAccount(
                It.Is<int>(user => user.Equals(1024)),
                It.Is<string>(name => name.Equals("Checking")),
                It.Is<string>(description => description.Equals("Checking Account"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateAccount()
        {
            var updated = await Service.UpdateAccount(2, "Transaction", "Transaction Account");

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateAccount(
                It.Is<int>(id => id.Equals(2)),
                It.Is<string>(lastName => lastName.Equals("Transaction")),
                It.Is<string>(phone => phone.Equals("Transaction Account"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteAccount()
        {
            var deleted = await Service.DeleteAccount(2);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteAccount(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}