using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class TransactionRepository : Repository, ITransactionRepository
    {
        public IAccountRepository Account { get; }

        public ICategoryRepository Category { get; }

        public ISubCategoryRepository SubCategory { get; }

        public ITransactionTypeRepository TransactionType { get; }

        public TransactionRepository(DatabaseContext context,
                IAccountRepository account,
                ICategoryRepository category,
                ISubCategoryRepository subCategory,
                ITransactionTypeRepository transactionType)
                : base(context)
        {
            Account = account;
            Category = category;
            SubCategory = subCategory;
            TransactionType = transactionType;
        }

        public async Task<int> GetTransactionsCount(int user, int account)
        {
            var result = 0;

            result = await Context.Transactions.CountAsync(i => i.User.Equals(user) && i.Account.Equals(account));

            return result;
        }

        public async Task<int> GetTransactionsByDayCount(int user, int account, int day, int month, int year)
        {
            var result = 0;

            result = await Context.Transactions.CountAsync(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Year.Equals(year) && i.Date.Month.Equals(month) && i.Date.Day.Equals(day));

            return result;
        }

        public async Task<int> GetTransactionsByMonthCount(int user, int account, int month, int year)
        {
            var result = 0;

            result = await Context.Transactions.CountAsync(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Year.Equals(year) && i.Date.Month.Equals(month));

            return result;
        }

        public async Task<int> GetTransactionsByYearCount(int user, int account, int year)
        {
            var result = 0;

            result = await Context.Transactions.CountAsync(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Year.Equals(year));

            return result;
        }

        public async Task<List<Transaction>> GetTransactions(int user, int account, int skip = 0, int count = 10)
        {
            var result = new List<Transaction>();

            var transactions = Context.Transactions.Where(i => i.User.Equals(user) && i.Account.Equals(account)).Skip(skip).Take(count).ToList();

            foreach (var i in transactions)
            {
                var item = new Transaction();

                item.ID = i.ID;
                item.Account = await Account.GetAccountById(i.Account);
                item.Category = await Category.GetCategoryById(i.Category);
                item.SubCategory = await SubCategory.GetSubCategoryById(i.SubCategory);
                item.Type = await TransactionType.GetTransactionTypeById(i.Type);
                item.Date = i.Date;
                item.Description = i.Description;
                item.Amount = i.Amount;

                result.Add(item);
            }

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByDay(int user, int account, int day, int month, int year, int skip = 0, int count = 10)
        {
            var result = new List<Transaction>();

            var transactions = Context.Transactions.Where(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Day.Equals(day) && i.Date.Month.Equals(month) && i.Date.Year.Equals(year)).Skip(skip).Take(count).ToList();

            foreach (var i in transactions)
            {
                var item = new Transaction();

                item.ID = i.ID;
                item.Account = await Account.GetAccountById(i.Account);
                item.Category = await Category.GetCategoryById(i.Category);
                item.SubCategory = await SubCategory.GetSubCategoryById(i.SubCategory);
                item.Type = await TransactionType.GetTransactionTypeById(i.Type);
                item.Date = i.Date;
                item.Description = i.Description;
                item.Amount = i.Amount;

                result.Add(item);
            }

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByMonth(int user, int account, int month, int year, int skip = 0, int count = 10)
        {
            var result = new List<Transaction>();

            var transactions = Context.Transactions.Where(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Month.Equals(month) && i.Date.Year.Equals(year)).Skip(skip).Take(count).ToList();

            foreach (var i in transactions)
            {
                var item = new Transaction();

                item.ID = i.ID;
                item.Account = await Account.GetAccountById(i.Account);
                item.Category = await Category.GetCategoryById(i.Category);
                item.SubCategory = await SubCategory.GetSubCategoryById(i.SubCategory);
                item.Type = await TransactionType.GetTransactionTypeById(i.Type);
                item.Date = i.Date;
                item.Description = i.Description;
                item.Amount = i.Amount;

                result.Add(item);
            }

            return result;
        }

        public async Task<List<Transaction>> GetTransactionsByYear(int user, int account, int year, int skip = 0, int count = 10)
        {
            var result = new List<Transaction>();

            var transactions = Context.Transactions.Where(i => i.User.Equals(user) && i.Account.Equals(account) && i.Date.Year.Equals(year)).Skip(skip).Take(count).ToList();

            foreach (var i in transactions)
            {
                var item = new Transaction();

                item.ID = i.ID;
                item.Account = await Account.GetAccountById(i.Account);
                item.Category = await Category.GetCategoryById(i.Category);
                item.SubCategory = await SubCategory.GetSubCategoryById(i.SubCategory);
                item.Type = await TransactionType.GetTransactionTypeById(i.Type);
                item.Date = i.Date;
                item.Description = i.Description;
                item.Amount = i.Amount;

                result.Add(item);
            }

            return result;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            Transaction result = null;

            var entity = await Context.Transactions.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                result = new Transaction();
                result.ID = entity.ID;
                result.Account = await Account.GetAccountById(entity.Account);
                result.Category = await Category.GetCategoryById(entity.Category);
                result.SubCategory = await SubCategory.GetSubCategoryById(entity.SubCategory);
                result.Type = await TransactionType.GetTransactionTypeById(entity.Type);
                result.Date = entity.Date;
                result.Description = entity.Description;
                result.Amount = entity.Amount;
            }

            return result;
        }

        public async Task<int> CreateTransaction(int user, int account, DateTime date, string description, int category, int subCategory, int type, double amount)
        {
            var result = 0;

            var entity = new Entity.Transaction()
            {
                User = user,
                Account = account,
                Date = date,
                Description = description,
                Category = category,
                SubCategory = subCategory,
                Type = type,
                Amount = amount
            };

            Context.Transactions.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateTransaction(int id, int user, int account, DateTime date, string description, int category, int subCategory, int type, double amount)
        {
            var result = false;

            var entity = await Context.Transactions.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.User = user;
                entity.Account = account;
                entity.Date = date;
                entity.Description = description;
                entity.Category = category;
                entity.SubCategory = subCategory;
                entity.Type = type;
                entity.Amount = amount;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var result = false;

            var entity = await Context.Transactions.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.Transactions.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}