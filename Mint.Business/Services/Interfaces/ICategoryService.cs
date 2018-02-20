using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface ICategoryService : IService
    {
        Task<List<Category>> GetCategories();

        Task<Category> GetCategoryById(int id);

        Task<Category> GetCategoryByName(string name);

        Task<int> CreateCategory(string name, string description);

        Task<bool> UpdateCategory(int id, string name, string description);

        Task<bool> DeleteCategory(int id);
    }
}