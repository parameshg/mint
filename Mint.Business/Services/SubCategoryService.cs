using Mint.Business.Services.Interfaces;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services
{
    public class SubCategoryService : Service, ISubCategoryService
    {
        public ISubCategoryRepository Repository { get; }

        public SubCategoryService(IServiceContext context, ISubCategoryRepository repository)
            : base(context)
        {
            Repository = repository;
        }

        public async Task<List<SubCategory>> GetSubCategories(int category)
        {
            var result = new List<SubCategory>();

            result.AddRange(await Repository.GetSubCategories(Context.User, category));

            return result;
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            SubCategory result = null;

            result = await Repository.GetSubCategoryById(id);

            return result;
        }

        public async Task<SubCategory> GetSubCategoryByName(string category, string name)
        {
            SubCategory result = null;

            result = await Repository.GetSubCategoryByName(category, name);

            return result;
        }

        public async Task<int> CreateSubCategory(int category, string name, string description)
        {
            var result = 0;

            result = await Repository.CreateSubCategory(Context.User, category, name, description);

            return result;
        }

        public async Task<bool> UpdateSubCategory(int id, int category, string name, string description)
        {
            var result = false;

            result = await Repository.UpdateSubCategory(id, category, name, description);

            return result;
        }

        public async Task<bool> DeleteSubCategory(int category, int id)
        {
            var result = false;

            result = await Repository.DeleteSubCategory(category, id);

            return result;
        }
    }
}