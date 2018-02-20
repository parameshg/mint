using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface ITransactionTypeService : IService
    {
        Task<List<TransactionType>> GetTransactionTypes();

        Task<TransactionType> GetTransactionTypeById(int id);

        Task<TransactionType> GetTransactionTypeByName(string name);
    }
}