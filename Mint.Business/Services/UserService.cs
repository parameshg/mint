using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class UserService : Service, IUserService
    {
        private IUserRepository Repository { get; }

        public UserService(IServiceContext context, IUserRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<User>> GetUsers(int page = 1, int count = 10)
        {
            var result = new List<User>();

            result.AddRange(await Repository.GetUsers(page * count, count));

            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            User result = null;

            result = await Repository.GetUserById(id);

            return result;
        }

        public async Task<int> CreateUser(string firstName, string lastName, string email, string phone)
        {
            var result = 0;

            result = await Repository.CreateUser(firstName, lastName, email, phone);

            return result;
        }

        public async Task<bool> UpdateUser(int id, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            result = await Repository.UpdateUser(id, firstName, lastName, email, phone);

            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = false;

            result = await Repository.DeleteUser(id);

            return result;
        }
    }
}