using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Business.Services.Interfaces
{
    public interface ISubCategoryService : IService
    {
        Task<List<SubCategory>> GetSubCategories(int category);

        Task<SubCategory> GetSubCategoryById(int id);

        Task<SubCategory> GetSubCategoryByName(string category, string name);

        Task<int> CreateSubCategory(int category, string name, string description);

        Task<bool> UpdateSubCategory(int id, int category, string name, string description);

        Task<bool> DeleteSubCategory(int category, int id);
    }
}