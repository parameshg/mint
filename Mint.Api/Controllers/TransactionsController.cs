using Microsoft.AspNetCore.Mvc;
using Mint.Api.Models;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        public ITransactionService Server { get; }

        public TransactionsController(ITransactionService server)
        {
            Server = server;
        }

        [HttpGet]
        [Route("transactions/count")]
        public async Task<int> GetTransactionsCount()
        {
            var result = 0;

            result = await Server.GetTransactionsCount();

            return result;
        }

        [HttpGet]
        [Route("transactions/year/count")]
        [Route("transactions/year/{year:int}/count")]
        public async Task<int> GetTransactionsByYearCount(int year = 0)
        {
            var result = 0;

            result = await Server.GetTransactionsByYearCount(year);

            return result;
        }

        [HttpGet]
        [Route("transactions/year/month/count")]
        [Route("transactions/year/{year:int}/month/{month:int}/count")]
        public async Task<int> GetTransactionsByMonthCount(int month = 0, int year = 0)
        {
            var result = 0;

            result = await Server.GetTransactionsByMonthCount(month, year);

            return result;
        }

        [HttpGet]
        [Route("transactions/year/month/day/count")]
        [Route("transactions/year/{year:int}/month/{month:int}/day/{day:int}/count")]
        public async Task<int> GetTransactionsByDayCount(int day = 0, int month = 0, int year = 0)
        {
            var result = 0;

            result = await Server.GetTransactionsByDayCount(day, month, year);

            return result;
        }

        [HttpGet]
        [Route("transactions")]
        [Route("transactions/{page:int}/{count:int}")]
        public async Task<List<Transaction>> GetTransactions(int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            result.AddRange(await Server.GetTransactions());

            return result;
        }

        [HttpGet]
        [Route("transactions/year")]
        [Route("transactions/year/{year:int}")]
        [Route("transactions/year/{year:int}/page/{page:int}/{count:int}")]
        public async Task<List<Transaction>> GetTransactionsByYear(int year = 0, int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            result.AddRange(await Server.GetTransactionsByYear(year, page, count));

            return result;
        }

        [HttpGet]
        [Route("transactions/year/month")]
        [Route("transactions/year/{year:int}/month/{month:int}")]
        [Route("transactions/year/{year:int}/month/{month:int}/page/{page:int}/{count:int}")]
        public async Task<List<Transaction>> GetTransactionsByMonth(int month = 0, int year = 0, int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            result.AddRange(await Server.GetTransactionsByMonth(month, year, page, count));

            return result;
        }

        [HttpGet]
        [Route("transactions/year/month/day")]
        [Route("transactions/year/{year:int}/month/{month:int}/day/{day:int}")]
        [Route("transactions/year/{year:int}/month/{month:int}/day/{day:int}/page/{page:int}/{count:int}")]
        public async Task<List<Transaction>> GetTransactionsByDay(int day = 0, int month = 0, int year = 0, int page = 1, int count = 10)
        {
            var result = new List<Transaction>();

            result.AddRange(await Server.GetTransactionsByDay(day, month, year, page, count));

            return result;
        }

        [HttpGet]
        [Route("transactions/{id:int}")]
        public async Task<Transaction> GetTransactionById(int id)
        {
            Transaction result = null;

            result = await Server.GetTransactionById(id);

            return result;
        }

        [HttpPost]
        [Route("transactions")]
        public async Task<int> CreateUser([FromBody] CreateTransactionModel model)
        {
            var result = 0;

            result = await Server.CreateTransaction(model.Date, model.Description, model.Category, model.SubCategory, model.Type, model.Amount);

            return result;
        }

        [HttpPut]
        [Route("transactions")]
        public async Task<bool> UpdateTransaction([FromBody] UpdateTransactionModel model)
        {
            var result = false;

            result = await Server.UpdateTransaction(model.ID, model.Date, model.Description, model.Category, model.SubCategory, model.Type, model.Amount);

            return result;
        }

        [HttpDelete]
        [Route("transactions/{id:int}")]
        public async Task<bool> DeleteTransaction(int id)
        {
            var result = false;

            result = await Server.DeleteTransaction(id);

            return result;
        }
    }
}