using Microsoft.AspNetCore.Mvc;
using Mint.Api.Models;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    public class UsersController : Controller
    {
        public IUserService Server { get; }

        public UsersController(IUserService server)
        {
            Server = server;
        }

        [HttpGet("users")]
        [Route("pages/{page:int}/{count:int}")]
        public async Task<List<User>> GetUsers(int page = 1, int count = 10)
        {
            var result = new List<User>();

            result.AddRange(await Server.GetUsers(page, count));

            return result;
        }

        [HttpGet("users/{id:int}")]
        public async Task<User> GetUserById(int id)
        {
            User result = null;

            result = await Server.GetUserById(id);

            return result;
        }

        [HttpPost("users")]
        public async Task<int> CreateUser([FromBody] CreateUserModel model)
        {
            var result = 0;

            result = await Server.CreateUser(model.FirstName, model.LastName, model.Email, model.Phone);

            return result;
        }

        [HttpPut("users")]
        public async Task<bool> UpdateUser([FromBody] UpdateUserModel model)
        {
            var result = false;

            result = await Server.UpdateUser(model.ID, model.FirstName, model.LastName, model.Email, model.Phone);

            return result;
        }

        [HttpDelete("users/{id:int}")]
        public async Task<bool> DeleteUser(int id)
        {
            var result = false;

            result = await Server.DeleteUser(id);

            return result;
        }
    }
}