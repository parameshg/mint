using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository
    {
        Task<List<Category>> GetCategories(int user);

        Task<Category> GetCategoryById(int id);

        Task<Category> GetCategoryByName(string name);

        Task<int> CreateCategory(int user, string name, string description);

        Task<bool> UpdateCategory(int id, string name, string description);

        Task<bool> DeleteCategory(int id);
    }
}