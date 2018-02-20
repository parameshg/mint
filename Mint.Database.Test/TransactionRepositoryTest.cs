using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class TransactionRepositoryTest : RepositoryTest
    {
        private ITransactionRepository Repository { get; set; }

        private int UserId { get; set; }
        private int AccountId { get; set; }
        private int CategoryId { get; set; }
        private int SubCategoryId { get; set; }
        private int TransactionTypeId { get; set; }
        private int TransactionId { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Repository = new TransactionRepository(Context, new AccountRepository(Context), new CategoryRepository(Context), new SubCategoryRepository(Context), new TransactionTypeRepository(Context));

            var user = new Entity.User() { FirstName = "test", LastName = "test", Email = "test@test.com", Phone = "123-123-1234" };
            Context.Users.Add(user);
            Context.SaveChanges();

            var account = new Entity.Account() { User = user.ID, Name = "test", Description = "test" };
            Context.Accounts.Add(account);
            Context.SaveChanges();

            var category = new Entity.Category() { User = user.ID, Name = "test", Description = "test" };
            Context.Categories.Add(category);
            Context.SaveChanges();

            var subCategory = new Entity.SubCategory() { User = user.ID, Category = category.ID, Name = "test", Description = "test" };
            Context.SubCategories.Add(subCategory);
            Context.SaveChanges();

            var type = new Entity.TransactionType() { Name = "test", Description = "test" };
            Context.TransactionTypes.Add(type);
            Context.SaveChanges();

            UserId = user.ID;
            AccountId = account.ID;
            CategoryId = category.ID;
            SubCategoryId = subCategory.ID;
            TransactionTypeId = type.ID;
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestCreateTransaction()
        {
            var amount = new Random().NextDouble() * new Random().Next(100, 999);

            TransactionId = await Repository.CreateTransaction(UserId, AccountId, DateTime.Today, "test", CategoryId, SubCategoryId, TransactionTypeId, amount);
            Assert.AreNotEqual(TransactionId, 0);

            var transaction = await Repository.GetTransactionById(TransactionId);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Account.ID, AccountId);
            Assert.AreEqual(transaction.Date, DateTime.Today);
            Assert.AreEqual(transaction.Description, "test");
            Assert.AreEqual(transaction.Amount, amount);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestUpdateTransaction()
        {
            var amount = new Random().NextDouble() * new Random().Next(100, 999);

            var transactions = await Repository.GetTransactions(UserId, AccountId);
            Assert.IsNotNull(transactions);
            Assert.AreNotEqual(transactions.Count, 0);

            var transaction = transactions.FirstOrDefault(i => i.Description.Equals("test"));
            Assert.IsNotNull(transaction);

            var updated = await Repository.UpdateTransaction(transaction.ID, UserId, AccountId, DateTime.Today, "testDescription2", CategoryId, SubCategoryId, TransactionTypeId, amount);
            Assert.IsTrue(updated);

            transaction = await Repository.GetTransactionById(transaction.ID);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Account, AccountId);
            Assert.AreEqual(transaction.Date, DateTime.Today);
            Assert.AreEqual(transaction.Description, "test2");
            Assert.AreEqual(transaction.Amount, amount);
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestGetTransactions()
        {
            var transactions = await Repository.GetTransactions(UserId, AccountId);
            Assert.IsNotNull(transactions);
            Assert.AreNotEqual(transactions.Count, 0);
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestGetTransactionById()
        {
            var transactions = await Repository.GetTransactions(UserId, AccountId);
            Assert.IsNotNull(transactions);
            Assert.AreNotEqual(transactions.Count, 0);

            var id = transactions.FirstOrDefault()?.ID;
            Assert.IsTrue(id.HasValue);

            if (id.HasValue)
            {
                var type = await Repository.GetTransactionById(id.Value);
                Assert.IsNotNull(type);
            }
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteTransaction()
        {
            var transactions = await Repository.GetTransactions(UserId, AccountId);
            Assert.IsNotNull(transactions);
            Assert.AreNotEqual(transactions.Count, 0);

            var transaction = transactions.FirstOrDefault(i => i.Description.Equals("testDescription2"));
            Assert.IsNotNull(transaction);

            var deleted = await Repository.DeleteTransaction(transaction.ID);
            Assert.IsTrue(deleted);

            transaction = await Repository.GetTransactionById(transaction.ID);
            Assert.IsNull(transaction);
        }

        [TestCleanup]
        public void Clean()
        {
            Context.Transactions.Remove(Context.Transactions.FirstOrDefault(i => i.ID.Equals(TransactionId)));
            Context.Accounts.Remove(Context.Accounts.FirstOrDefault(i => i.ID.Equals(AccountId)));
            Context.Categories.Remove(Context.Categories.FirstOrDefault(i => i.ID.Equals(CategoryId)));
            Context.SubCategories.Remove(Context.SubCategories.FirstOrDefault(i => i.ID.Equals(SubCategoryId)));
            Context.TransactionTypes.Remove(Context.TransactionTypes.FirstOrDefault(i => i.ID.Equals(TransactionTypeId)));
            Context.SaveChanges();
        }
    }
}