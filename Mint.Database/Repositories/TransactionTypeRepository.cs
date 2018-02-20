using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class TransactionTypeRepository : Repository, ITransactionTypeRepository
    {
        public TransactionTypeRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<TransactionType>> GetTransactionTypes()
        {
            var result = new List<TransactionType>();

            (await Context.TransactionTypes.ToListAsync()).ForEach(item =>
            {
                result.Add(new TransactionType()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            });

            return result;
        }

        public async Task<TransactionType> GetTransactionTypeById(int id)
        {
            TransactionType result = null;

            var entity = await Context.TransactionTypes.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.TransactionType, TransactionType>(entity);

            return result;
        }

        public async Task<TransactionType> GetTransactionTypeByName(string name)
        {
            TransactionType result = null;

            var entity = await Context.TransactionTypes.FirstOrDefaultAsync(i => i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (entity != null)
                result = Mapper.Map<Entity.TransactionType, TransactionType>(entity);

            return result;
        }

        public async Task<int> CreateTransactionType(string name, string description)
        {
            var result = 0;

            var entity = new Entity.TransactionType()
            {
                Name = name,
                Description = description
            };

            Context.TransactionTypes.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateTransactionType(int id, string name, string description)
        {
            var result = false;

            var entity = await Context.TransactionTypes.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.Name = name;
                entity.Description = description;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteTransactionType(int id)
        {
            var result = false;

            var entity = await Context.TransactionTypes.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.TransactionTypes.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}