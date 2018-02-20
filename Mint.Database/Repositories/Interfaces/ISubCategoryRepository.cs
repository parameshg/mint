
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories.Interfaces
{
    public interface ISubCategoryRepository : IRepository
    {
        Task<List<SubCategory>> GetSubCategories(int user, int category);

        Task<SubCategory> GetSubCategoryById(int id);

        Task<SubCategory> GetSubCategoryByName(string category, string name);

        Task<int> CreateSubCategory(int user, int category, string name, string description);

        Task<bool> UpdateSubCategory(int id, int category, string name, string description);

        Task<bool> DeleteSubCategory(int category, int id);
    }
}