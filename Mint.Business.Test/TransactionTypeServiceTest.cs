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
    public class TransactionTypeServiceTest : ServiceTest
    {
        private Mock<ITransactionTypeRepository> Repository { get; set; }

        private ITransactionTypeService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var TransactionType = new TransactionType() { ID = 2, Name = "Income", Description = "Income or Inbound" };

            Repository = new Mock<ITransactionTypeRepository>();

            Repository.Setup(i => i.GetTransactionTypes()).ReturnsAsync(new List<TransactionType>(new TransactionType[] { TransactionType }));

            Repository.Setup(i => i.GetTransactionTypeById(It.Is<int>(o => o.Equals(2)))).ReturnsAsync(TransactionType);

            Repository.Setup(i => i.CreateTransactionType(
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateTransactionType(
                It.IsAny<int>(), // id
                It.IsAny<string>(), // name
                It.IsAny<string>())) // description
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteTransactionType(It.IsAny<int>())).ReturnsAsync(true);

            Service = new TransactionTypeService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetTransactionTypes()
        {
            var users = await Service.GetTransactionTypes();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 1);

            Repository.Verify(i => i.GetTransactionTypes(), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetTransactionTypeById()
        {
            var TransactionType = await Service.GetTransactionTypeById(2);

            Assert.IsNotNull(TransactionType);

            Repository.Verify(i => i.GetTransactionTypeById(It.Is<int>(id => id.Equals(2))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}