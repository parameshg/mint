using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface ITransactionService : IService
    {
        Task<int> GetTransactionsCount();

        Task<int> GetTransactionsByDayCount(int day = 0, int month = 0, int year = 0);

        Task<int> GetTransactionsByMonthCount(int month = 0, int year = 0);

        Task<int> GetTransactionsByYearCount(int year = 0);

        Task<Transaction> GetTransactionById(int id);

        Task<List<Transaction>> GetTransactions(int page = 1, int count = 10);

        Task<List<Transaction>> GetTransactionsByYear(int year = 0, int skip = 0, int count = 10);

        Task<List<Transaction>> GetTransactionsByMonth(int month = 0, int year = 0, int skip = 0, int count = 10);

        Task<List<Transaction>> GetTransactionsByDay(int day = 0, int month = 0, int year = 0, int skip = 0, int count = 10);

        Task<int> CreateTransaction(DateTime date, string description, int category, int subCategory, int type, double amount);

        Task<bool> UpdateTransaction(int id, DateTime date, string description, int category, int subCategory, int type, double amount);

        Task<bool> DeleteTransaction(int id);
    }
}