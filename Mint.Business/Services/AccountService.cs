using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class AccountService : Service, IAccountService
    {
        public IAccountRepository Repository { get; }

        public AccountService(IServiceContext context, IAccountRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var result = new List<Account>();

            result.AddRange(await Repository.GetAccounts(Context.User));

            return result;
        }

        public async Task<Account> GetAccountById(int id)
        {
            Account result = null;

            result = await Repository.GetAccountById(id);

            return result;
        }

        public async Task<int> CreateAccount(string name, string description)
        {
            var result = 0;

            result = await Repository.CreateAccount(Context.User, name, description);

            return result;
        }

        public async Task<bool> UpdateAccount(int id, string name, string description)
        {
            var result = false;

            result = await Repository.UpdateAccount(id, name, description);

            return result;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var result = false;

            result = await Repository.DeleteAccount(id);

            return result;
        }
    }
}