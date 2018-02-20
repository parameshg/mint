using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ITransactionTypeRepository : IRepository
    {
        Task<List<TransactionType>> GetTransactionTypes();

        Task<TransactionType> GetTransactionTypeById(int id);

        Task<TransactionType> GetTransactionTypeByName(string name);

        Task<int> CreateTransactionType(string name, string description);

        Task<bool> UpdateTransactionType(int id, string name, string description);

        Task<bool> DeleteTransactionType(int id);
    }
}