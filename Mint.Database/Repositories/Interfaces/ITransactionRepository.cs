using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository
    {
        Task<int> GetTransactionsCount(int user, int account);

        Task<int> GetTransactionsByDayCount(int user, int account, int day, int month, int year);

        Task<int> GetTransactionsByMonthCount(int user, int account, int month, int year);

        Task<int> GetTransactionsByYearCount(int user, int account, int year);

        Task<List<Transaction>> GetTransactions(int user, int account, int skip = 0, int count = 10);

        Task<List<Transaction>> GetTransactionsByDay(int user, int account, int day, int month, int year, int skip = 0, int count = 10);

        Task<List<Transaction>> GetTransactionsByMonth(int user, int account, int month, int year, int skip = 0, int count = 10);

        Task<List<Transaction>> GetTransactionsByYear(int user, int account, int year, int skip = 0, int count = 10);

        Task<Transaction> GetTransactionById(int id);

        Task<int> CreateTransaction(int user, int account, DateTime date, string description, int category, int subCategory, int type, double amount);

        Task<bool> UpdateTransaction(int id, int user, int account, DateTime date, string description, int category, int subCategory, int type, double amount);

        Task<bool> DeleteTransaction(int id);
    }
}