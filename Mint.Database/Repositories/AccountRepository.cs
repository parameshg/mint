using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        public AccountRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<Account>> GetAccounts(int user)
        {
            var result = new List<Account>();

            (await Context.Accounts.Where(i => i.User.Equals(user)).ToListAsync()).ForEach(item =>
            {
                result.Add(new Account()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            });

            return result;
        }

        public async Task<Account> GetAccountById(int id)
        {
            Account result = null;

            var entity = await Context.Accounts.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.Account, Account>(entity);

            return result;
        }

        public async Task<int> CreateAccount(int user, string name, string description)
        {
            var result = 0;

            var entity = new Entity.Account()
            {
                User = user,
                Name = name,
                Description = description
            };

            Context.Accounts.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateAccount(int id, string name, string description)
        {
            var result = false;

            var entity = await Context.Accounts.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.Name = name;
                entity.Description = description;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var result = false;

            var entity = await Context.Accounts.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.Accounts.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}