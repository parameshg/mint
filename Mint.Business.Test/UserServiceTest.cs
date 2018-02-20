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
    public class UserServiceTest : ServiceTest
    {
        private Mock<IUserRepository> Repository { get; set; }

        private IUserService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var user = new User() { ID = 1024, FirstName = "John", LastName = "Smith", Email = "john@example.com", Phone = "123-123-1234" };

            Repository = new Mock<IUserRepository>();

            Repository.Setup(i => i.GetUsers(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<User>(new User[] { user }));

            Repository.Setup(i => i.GetUserById(It.Is<int>(o => o.Equals(1024)))).ReturnsAsync(user);

            Repository.Setup(i => i.CreateUser(
                It.IsAny<string>(), // firstName
                It.IsAny<string>(), // lastName
                It.IsAny<string>(), // email
                It.IsAny<string>())) // phone
                .ReturnsAsync(new Random().Next());

            Repository.Setup(i => i.UpdateUser(
                It.IsAny<int>(), // id
                It.IsAny<string>(), // firstName
                It.IsAny<string>(), // lastName
                It.IsAny<string>(), // email
                It.IsAny<string>())) // phone
                .ReturnsAsync(true);

            Repository.Setup(i => i.DeleteUser(It.IsAny<int>())).ReturnsAsync(true);

            Service = new UserService(Context.Object, Repository.Object);
        }

        [TestMethod]
        public async Task TestGetUsers()
        {
            var users = await Service.GetUsers();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 1);

            Repository.Verify(i => i.GetUsers(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestGetUserById()
        {
            var user = await Service.GetUserById(1024);

            Assert.IsNotNull(user);

            Repository.Verify(i => i.GetUserById(It.Is<int>(id => id.Equals(1024))), Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestCreateUser()
        {
            var created = await Service.CreateUser("John", "Smith", "john@example.com", "123-123-1234");

            Assert.IsTrue(created > 0);

            Repository.Verify(i => i.CreateUser(
                It.Is<string>(firstName => firstName.Equals("John")),
                It.Is<string>(lastName => lastName.Equals("Smith")),
                It.Is<string>(email => email.Equals("john@example.com")),
                It.Is<string>(phone => phone.Equals("123-123-1234"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestUpdateUser()
        {
            var updated = await Service.UpdateUser(1024, "John", "Smith", "john@example.com", "123-123-1234");

            Assert.IsTrue(updated);

            Repository.Verify(i => i.UpdateUser(
                It.Is<int>(id => id.Equals(1024)),
                It.Is<string>(firstName => firstName.Equals("John")),
                It.Is<string>(lastName => lastName.Equals("Smith")),
                It.Is<string>(email => email.Equals("john@example.com")),
                It.Is<string>(phone => phone.Equals("123-123-1234"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public async Task TestDeleteUser()
        {
            var deleted = await Service.DeleteUser(1024);

            Assert.IsTrue(deleted);

            Repository.Verify(i => i.DeleteUser(It.Is<int>(id => id.Equals(1024))), Times.Exactly(1));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}