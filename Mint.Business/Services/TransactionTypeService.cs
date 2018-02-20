using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class TransactionTypeService : Service, ITransactionTypeService
    {
        public ITransactionTypeRepository Repository { get; }

        public TransactionTypeService(IServiceContext context, ITransactionTypeRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<TransactionType>> GetTransactionTypes()
        {
            var result = new List<TransactionType>();

            result.AddRange(await Repository.GetTransactionTypes());

            return result;
        }

        public async Task<TransactionType> GetTransactionTypeById(int id)
        {
            TransactionType result = null;

            result = await Repository.GetTransactionTypeById(id);

            return result;
        }

        public async Task<TransactionType> GetTransactionTypeByName(string name)
        {
            TransactionType result = null;

            result = await Repository.GetTransactionTypeByName(name);

            return result;
        }
    }
}