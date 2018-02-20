using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<List<User>> GetUsers(int page = 1, int count = 10);

        Task<User> GetUserById(int id);

        Task<int> CreateUser(string firstName, string lastName, string email, string phone);

        Task<bool> UpdateUser(int id, string firstName, string lastName, string email, string phone);

        Task<bool> DeleteUser(int id);
    }
}