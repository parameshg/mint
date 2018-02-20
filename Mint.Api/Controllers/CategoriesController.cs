using Microsoft.AspNetCore.Mvc;
using Mint.Api.Models;
using Mint.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Api.Controllers
{
    [Produces("application/json")]
    [Route("categories")]
    public class CategoriesController : Controller
    {
        public ICategoryService Category { get; }

        public ISubCategoryService SubCategory { get; }

        public CategoriesController(ICategoryService category, ISubCategoryService subCategory)
        {
            Category = category;
            SubCategory = subCategory;
        }

        [HttpGet("")]
        public async Task<List<Category>> GetCategories()
        {
            var result = new List<Category>();

            result.AddRange(await Category.GetCategories());

            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<Category> GetCategoryById(int id)
        {
            Category result = null;

            result = await Category.GetCategoryById(id);

            return result;
        }

        [HttpGet("next/{category:int}")]
        public async Task<List<SubCategory>> GetSubCategories(int category)
        {
            var result = new List<SubCategory>();

            result.AddRange(await SubCategory.GetSubCategories(category));

            return result;
        }

        [HttpGet("next/{category:int}/{id:int}")]
        public async Task<SubCategory> GetSubCategoryById(int category, int id)
        {
            SubCategory result = null;

            result = await SubCategory.GetSubCategoryById(id);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateUser([FromBody] CreateCategoryModel model)
        {
            var result = 0;

            if (model.Parent.Equals(0))
                result = await Category.CreateCategory(model.Name, model.Description);
            else
                result = await SubCategory.CreateSubCategory(model.Parent, model.Name, model.Description);

            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateCategory([FromBody] UpdateCategoryModel model)
        {
            var result = false;

            if (model.Parent.Equals(0))
                result = await Category.UpdateCategory(model.ID, model.Name, model.Description);
            else
                result = await SubCategory.UpdateSubCategory(model.ID, model.Parent, model.Name, model.Description);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteCategory(int id)
        {
            var result = false;

            result = await Category.DeleteCategory(id);

            return result;
        }

        [HttpDelete("{category:int}/{id:int}")]
        public async Task<bool> DeleteSubCategory(int category, int id)
        {
            var result = false;

            result = await SubCategory.DeleteSubCategory(category, id);

            return result;
        }
    }
}