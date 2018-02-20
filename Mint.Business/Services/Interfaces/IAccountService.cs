using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface IAccountService : IService
    {
        Task<List<Account>> GetAccounts();

        Task<Account> GetAccountById(int id);

        Task<int> CreateAccount(string name, string description);

        Task<bool> UpdateAccount(int id, string name, string description);

        Task<bool> DeleteAccount(int id);
    }
}