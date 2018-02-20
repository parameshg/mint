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
    public class TransactionServiceTest : ServiceTest
    {
        private Mock<ITransactionRepository> Repository { get; set; }

        private ITransactionService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var transaction = new Transaction()
            {
                ID = 10,
                Date = DateTime.Today,
                Account = new Account() { ID = 1, Name = "Savings", Description = "Savings Account" },
                Type = new TransactionType() { ID = 2, Name = "Expense", Description = "Expense" },
                Category = new Category() { ID = 3, Name = "Entertainment", Description = "Entertainment" },
                SubCategory = new SubCategory() { ID = 4, Name = "Movies", Description = "Movies" },
                Description = "iMax Movies",
                Amount = 100.99
            };

            Repository = new Mock<ITransactionRepository>();

            Repository.Setup(i => i.GetTransactions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Transaction>(new Transaction[] { transaction }));

            Repository.Setup(i => i.GetTransactionById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(transaction);

            Repository.Setup(i => i.CreateTransaction(
                It.IsAny<int>(), // user
                It.IsAny<int>(), // account
                It.IsAny<DateTime>(), // date
                It.IsAny<string>(), // description
                It.IsAny<int>(), // category
                It.IsAny<int>(), // sub-category
                It.IsAny<int>(), // type
                It.IsAny<double>())) // amount
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateTransaction(
                It.IsAny<int>(), // id
                It.IsAny<int>(), // user
                It.IsAny<int>(), // account
                It.IsAny<DateTime>(), // date
                It.IsAny<string>(), // description
                It.IsAny<int>(), // category
                It.IsAny<int>(), // sub-category
                It.IsAny<int>(), // type
                It.IsAny<double>())) // amount
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteTransaction(It.IsAny<int>())).ReturnsAsync(true);

            Service = new TransactionService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetTransactions()
        {
            var Transactions = await Service.GetTransactions(2);

            Assert.IsNotNull(Transactions);
            Assert.AreEqual(Transactions.Count, 1);

            Repository.Verify(i => i.GetTransactions(It.Is<int>(o => o.Equals(1024)), It.Is<int>(o => o.Equals(2)), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetTransactionById()
        {
            var Transaction = await Service.GetTransactionById(2);

            Assert.IsNotNull(Transaction);

            Repository.Verify(i => i.GetTransactionById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateTransaction()
        {
            var created = await Service.CreateTransaction(DateTime.Today, "iMax Movies", 2, 3, 4, 100.99);

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateTransaction(
                It.Is<int>(user => user.Equals(1024)),
                It.Is<int>(account => account.Equals(1)),
                It.Is<DateTime>(date => date.Equals(DateTime.Today)),
                It.Is<string>(description => description.Equals("iMax Movies")),
                It.Is<int>(category => category.Equals(2)),
                It.Is<int>(category => category.Equals(3)),
                It.Is<int>(type => type.Equals(4)),
                It.Is<double>(amount => amount.Equals(100.99))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateTransaction()
        {
            var updated = await Service.UpdateTransaction(10, DateTime.Today, "iMax Movies", 2, 3, 4, 100.99);

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateTransaction(
                It.Is<int>(id => id.Equals(10)),
                It.Is<int>(user => user.Equals(1024)),
                It.Is<int>(account => account.Equals(1)),
                It.Is<DateTime>(date => date.Equals(DateTime.Today)),
                It.Is<string>(description => description.Equals("iMax Movies")),
                It.Is<int>(category => category.Equals(2)),
                It.Is<int>(category => category.Equals(3)),
                It.Is<int>(type => type.Equals(4)),
                It.Is<double>(amount => amount.Equals(100.99))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteTransaction()
        {
            var deleted = await Service.DeleteTransaction(10);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteTransaction(It.Is<int>(id => id.Equals(10))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}