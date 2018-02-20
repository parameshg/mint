using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using Mint.Reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class ReportService : Service, IReportService
    {
        public ITransactionRepository Transactions { get; }

        public ReportService(IServiceContext context, ITransactionRepository transactions)
            : base(context)
        {
            Transactions = transactions;
        }


        public async Task<CategoryWiseReport> GetCategoryWiseReport(int year, int month = 0)
        {
            var result = new CategoryWiseReport();

            result.Month = month;
            result.Year = year;

            var count = 0;

            List<Transaction> transactions = null;

            if (month.Equals(0))
            {
                count = await Transactions.GetTransactionsByYearCount(Context.User, Context.Account, year);

                transactions = await Transactions.GetTransactionsByYear(Context.User, Context.Account, year, 0, count);
            }
            else
            {
                count = await Transactions.GetTransactionsByMonthCount(Context.User, Context.Account, year, month);

                transactions = await Transactions.GetTransactionsByMonth(Context.User, Context.Account, year, month, 0, count);
            }

            foreach (var i in transactions)
            {
                if (i.Type.Name.Equals("Expense", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (result.Expense.ContainsKey(i.Category))
                        result.Expense[i.Category] += i.Amount;
                    else
                        result.Expense.Add(i.Category, i.Amount);
                }

                if (i.Type.Name.Equals("Income", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (result.Income.ContainsKey(i.Category))
                        result.Income[i.Category] += i.Amount;
                    else
                        result.Income.Add(i.Category, i.Amount);
                }
            }

            return result;
        }

        public async Task<SummaryReport> GetSummaryReport(int year, int month = 0)
        {
            var result = new SummaryReport();

            result.Month = month;
            result.Year = year;

            var count = 0;

            List<Transaction> transactions = null;

            if (month.Equals(0))
            {
                count = await Transactions.GetTransactionsByYearCount(Context.User, Context.Account, year);

                transactions = await Transactions.GetTransactionsByYear(Context.User, Context.Account, year, 0, count);
            }
            else
            {
                count = await Transactions.GetTransactionsByMonthCount(Context.User, Context.Account, year, month);

                transactions = await Transactions.GetTransactionsByMonth(Context.User, Context.Account, year, month, 0, count);
            }

            foreach (var i in transactions)
            {
                if (i.Type.Name.Equals("Expense", StringComparison.CurrentCultureIgnoreCase))
                    result.Expense += i.Amount;

                if (i.Type.Name.Equals("Income", StringComparison.CurrentCultureIgnoreCase))
                    result.Income += i.Amount;
            }

            return result;
        }
    }
}