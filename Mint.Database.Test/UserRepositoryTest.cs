using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mint.Database.Repositories;
using Mint.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Test
{
    [TestClass]
    public class UserRepositoryTest : RepositoryTest
    {
        private IUserRepository Repository { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Repository = new UserRepository(Context);
        }

        [Priority(1)]
        [TestMethod]
        public async Task TestGetUsers()
        {
            var users = await Repository.GetUsers();
            Assert.IsNotNull(users);
            Assert.AreNotEqual(users.Count, 0);
        }

        [Priority(2)]
        [TestMethod]
        public async Task TestGetUserById()
        {
            var users = await Repository.GetUsers();
            Assert.IsNotNull(users);
            Assert.AreNotEqual(users.Count, 0);

            var id = users.FirstOrDefault()?.ID;

            if (id.HasValue)
            {
                var user = await Repository.GetUserById(id.Value);
                Assert.IsNotNull(user);
            }
        }

        [Priority(3)]
        [TestMethod]
        public async Task TestCreateUser()
        {
            var id = await Repository.CreateUser("test", "test", "test@test.com", "123-123-1234");
            Assert.AreNotEqual(id, 0);

            var user = await Repository.GetUserById(id);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.FirstName, "test");
            Assert.AreEqual(user.LastName, "test");
            Assert.AreEqual(user.Email, "test@test.com");
            Assert.AreEqual(user.Phone, "123-123-1234");
        }

        [Priority(4)]
        [TestMethod]
        public async Task TestUpdateUser()
        {
            var users = await Repository.GetUsers();
            Assert.IsNotNull(users);
            Assert.AreNotEqual(users.Count, 0);

            var user = users.FirstOrDefault(i => i.FirstName.Equals("test"));
            Assert.IsNotNull(user);

            var updated = await Repository.UpdateUser(user.ID, "test2", "test2", "test2@test.com", "321-321-4321");
            Assert.IsTrue(updated);

            user = await Repository.GetUserById(user.ID);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.FirstName, "test2");
            Assert.AreEqual(user.LastName, "test2");
            Assert.AreEqual(user.Email, "test2@test.com");
            Assert.AreEqual(user.Phone, "321-321-4321");
        }

        [Priority(5)]
        [TestMethod]
        public async Task TestDeleteUser()
        {
            var users = await Repository.GetUsers();
            Assert.IsNotNull(users);
            Assert.AreNotEqual(users.Count, 0);

            var user = users.FirstOrDefault(i => i.FirstName.Equals("test2"));
            Assert.IsNotNull(user);

            var deleted = await Repository.DeleteUser(user.ID);
            Assert.IsTrue(deleted);

            user = await Repository.GetUserById(user.ID);
            Assert.IsNull(user);
        }

        [TestCleanup]
        public void Clean()
        {

        }
    }
}