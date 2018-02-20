using Microsoft.AspNetCore.Mvc;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    [Route("types")]
    public class TypesController : Controller
    {
        public ITransactionTypeService Server { get; }

        public TypesController(ITransactionTypeService server)
        {
            Server = server;
        }

        [HttpGet("")]
        public async Task<List<TransactionType>> GetTypes()
        {
            var result = new List<TransactionType>();

            result.AddRange(await Server.GetTransactionTypes());

            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<TransactionType> GetTypeById(int id)
        {
            TransactionType result = null;

            result = await Server.GetTransactionTypeById(id);

            return result;
        }

        [HttpGet("name/{name}")]
        public async Task<TransactionType> GetTypeByName(string name)
        {
            TransactionType result = null;

            result = await Server.GetTransactionTypeByName(name);

            return result;
        }
    }
}