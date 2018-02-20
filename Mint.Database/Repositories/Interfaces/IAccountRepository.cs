
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository
    {
        Task<List<Account>> GetAccounts(int user);

        Task<Account> GetAccountById(int id);

        Task<int> CreateAccount(int user, string name, string description);

        Task<bool> UpdateAccount(int id, string name, string description);

        Task<bool> DeleteAccount(int id);
    }
}