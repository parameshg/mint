using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class SubCategoryRepository : Repository, ISubCategoryRepository
    {
        public SubCategoryRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<SubCategory>> GetSubCategories(int user, int category)
        {
            var result = new List<SubCategory>();

            (await Context.SubCategories.Where(i => i.User.Equals(user) && i.Category.Equals(category)).ToListAsync()).ForEach(item =>
            {
                result.Add(new SubCategory()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            });

            return result;
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            SubCategory result = null;

            var entity = await Context.SubCategories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.SubCategory, SubCategory>(entity);

            return result;
        }

        public async Task<SubCategory> GetSubCategoryByName(string category, string name)
        {
            SubCategory result = null;

            var parent = await Context.Categories.FirstOrDefaultAsync(i => i.Name.Equals(category, StringComparison.CurrentCultureIgnoreCase));

            var entity = await Context.SubCategories.FirstOrDefaultAsync(i => i.Category.Equals(parent.ID) && i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (entity != null)
                result = Mapper.Map<Entity.SubCategory, SubCategory>(entity);

            return result;
        }

        public async Task<int> CreateSubCategory(int user, int category, string name, string description)
        {
            var result = 0;

            var entity = new Entity.SubCategory()
            {
                User = user,
                Category = category,
                Name = name,
                Description = description
            };

            Context.SubCategories.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateSubCategory(int id, int category, string name, string description)
        {
            var result = false;

            var entity = await Context.SubCategories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.Category = category;
                entity.Name = name;
                entity.Description = description;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteSubCategory(int category, int id)
        {
            var result = false;

            var entity = await Context.SubCategories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.SubCategories.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}