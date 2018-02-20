using Microsoft.AspNetCore.Mvc;
using Mint.Api.Models;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    public class AccountsController : Controller
    {
        public IAccountService Server { get; }

        public AccountsController(IAccountService server)
        {
            Server = server;
        }

        [HttpGet("accounts")]
        public async Task<List<Account>> GetAccounts()
        {
            var result = new List<Account>();

            result.AddRange(await Server.GetAccounts());

            return result;
        }

        [HttpGet("accounts/{id:int}")]
        public async Task<Account> GetAccountById(int id)
        {
            Account result = null;

            result = await Server.GetAccountById(id);

            return result;
        }

        [HttpPost("accounts")]
        public async Task<int> CreateAccount([FromBody] CreateAccountModel model)
        {
            var result = 0;

            result = await Server.CreateAccount(model.Name, model.Description);

            return result;
        }

        [HttpPut("accounts")]
        public async Task<bool> UpdateAccount([FromBody] UpdateAccountModel model)
        {
            var result = false;

            result = await Server.UpdateAccount(model.ID, model.Name, model.Description);

            return result;
        }

        [HttpDelete("accounts/{id:int}")]
        public async Task<bool> DeleteAccount(int id)
        {
            var result = false;

            result = await Server.DeleteAccount(id);

            return result;
        }
    }
}