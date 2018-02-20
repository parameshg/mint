using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public ICategoryRepository Repository { get; }

        public CategoryService(IServiceContext context, ICategoryRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<Category>> GetCategories()
        {
            var result = new List<Category>();

            result.AddRange(await Repository.GetCategories(Context.User));

            return result;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category result = null;

            result = await Repository.GetCategoryById(id);

            return result;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            Category result = null;

            result = await Repository.GetCategoryByName(name);

            return result;
        }

        public async Task<int> CreateCategory(string name, string description)
        {
            var result = 0;

            result = await Repository.CreateCategory(Context.User, name, description);

            return result;
        }

        public async Task<bool> UpdateCategory(int id, string name, string description)
        {
            var result = false;

            result = await Repository.UpdateCategory(id, name, description);

            return result;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var result = false;

            result = await Repository.DeleteCategory(id);

            return result;
        }
    }
}