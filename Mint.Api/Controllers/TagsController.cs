using Microsoft.AspNetCore.Mvc;
using Mint.Api.Models;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    [Route("tags")]
    public class TagsController : Controller
    {
        public ITagService Server { get; }

        public TagsController(ITagService server)
        {
            Server = server;
        }

        [HttpGet("")]
        public async Task<List<Tag>> GetTags()
        {
            var result = new List<Tag>();

            result.AddRange(await Server.GetTags());

            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<Tag> GetTagById(int id)
        {
            Tag result = null;

            result = await Server.GetTagById(id);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateUser([FromBody] CreateTagModel model)
        {
            var result = 0;

            result = await Server.CreateTag(model.Name, model.Description);

            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateTag([FromBody] UpdateTagModel model)
        {
            var result = false;

            result = await Server.UpdateTag(model.ID, model.Name, model.Description);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteTag(int id)
        {
            var result = false;

            result = await Server.DeleteTag(id);

            return result;
        }
    }
}