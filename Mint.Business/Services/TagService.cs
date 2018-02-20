using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class TagService : Service, ITagService
    {
        public ITagRepository Repository { get; }

        public TagService(IServiceContext context, ITagRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<Tag>> GetTags()
        {
            var result = new List<Tag>();

            result.AddRange(await Repository.GetTags(Context.User));

            return result;
        }

        public async Task<Tag> GetTagById(int id)
        {
            Tag result = null;

            result = await Repository.GetTagById(id);

            return result;
        }

        public async Task<int> CreateTag(string name, string description)
        {
            var result = 0;

            result = await Repository.CreateTag(Context.User, name, description);

            return result;
        }

        public async Task<bool> UpdateTag(int id, string name, string description)
        {
            var result = false;

            result = await Repository.UpdateTag(id, name, description);

            return result;
        }

        public async Task<bool> DeleteTag(int id)
        {
            var result = false;

            result = await Repository.DeleteTag(id);

            return result;
        }
    }
}