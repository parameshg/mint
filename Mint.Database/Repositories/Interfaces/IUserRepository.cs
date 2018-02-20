using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<List<User>> GetUsers(int skip = 0, int count = 10);

        Task<User> GetUserById(int id);

        Task<int> CreateUser(string firstName, string lastName, string email, string phone);

        Task<bool> UpdateUser(int id, string firstName, string lastName, string email, string phone);

        Task<bool> DeleteUser(int id);
    }
}