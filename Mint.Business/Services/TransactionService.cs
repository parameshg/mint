using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class TransactionService : Service, ITransactionService
    {
        public ITransactionRepository Repository { get; }

        public TransactionService(IServiceContext context, ITransactionRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<int> GetTransactionsCount()
        {
            var result = 0;

            result = await Repository.GetTransactionsCount(Context.User, Context.Account);

            return result;
        }

        public async Task<int> GetTransactionsByDayCount(int day = 0, int month = 0, int year = 0)
        {
            var result = 0;

            day = day.Equals(0) ? DateTime.Today.Day : day;
            month = month.Equals(0) ? DateTime.Today.Month : month;
            year = year.Equals(0) ? DateTime.Today.Year : year;

            result = await Repository.GetTransactionsByDayCount(Context.User, Context.Account, day, month, year);

            return result;
        }

        public async Task<int> GetTransactionsByMonthCount(int month = 0, int year = 0)
        {
            var result = 0;

            month = month.Equals(0) ? DateTime.Today.Month : month;
            year = year.Equals(0) ? DateTime.Today.Year : year;

            result = await Repository.GetTransactionsByMonthCount(Context.User, Context.Account, month, year);

            return result;
        }

        public async Task<int> GetTransactionsByYearCount(int year = 0)
        {
            var result = 0;

            year = year.Equals(0) ? DateTime.Today.Year : year;

            result = await Repository.GetTransactionsByYearCount(Context.User, Context.Account, year);

            return result;
        }

        public async Task<List<Transaction>> GetTransactions(int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            result.AddRange(await Repository.GetTransactions(Context.User, Context.Account, (page - 1) * count, count));

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByDay(int day = 0, int month = 0, int year = 0, int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            day = day.Equals(0) ? DateTime.Today.Day : day;
            month = month.Equals(0) ? DateTime.Today.Month : month;
            year = year.Equals(0) ? DateTime.Today.Year : year;

            result.AddRange(await Repository.GetTransactionsByDay(Context.User, Context.Account, day, month, year, (page - 1) * count, count));

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByMonth(int month = 0, int year = 0, int page = 0, int count = 10)
        {
            var result = new List<Transaction>();

            month = month.Equals(0) ? DateTime.Today.Month : month;
            year = year.Equals(0) ? DateTime.Today.Year : year;

            result.AddRange(await Repository.GetTransactionsByMonth(Context.User, Context.Account, month, year, (page - 1) * count, count));

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByYear(int year = 0, int page = 0, int count = 10)
        {
            var result = new List<Transaction>();

            year = year.Equals(0) ? DateTime.Today.Year : year;

            result.AddRange(await Repository.GetTransactionsByYear(Context.User, Context.Account, year, (page - 1) * count, count));

            return result;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            Transaction result = null;

            result = await Repository.GetTransactionById(id);

            return result;
        }

        public async Task<int> CreateTransaction(DateTime date, string description, int category, int subCategory, int type, double amount)
        {
            var result = 0;

            result = await Repository.CreateTransaction(Context.User, Context.Account, date, description, category, subCategory, type, amount);

            return result;
        }

        public async Task<bool> UpdateTransaction(int id, DateTime date, string description, int category, int subCategory, int type, double amount)
        {
            var result = false;

            result = await Repository.UpdateTransaction(id, Context.User, Context.Account, date, description, category, subCategory, type, amount);

            return result;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var result = false;

            result = await Repository.DeleteTransaction(id);

            return result;
        }
    }
}