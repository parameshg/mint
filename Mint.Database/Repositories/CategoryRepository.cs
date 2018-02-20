using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<Category>> GetCategories(int user)
        {
            var result = new List<Category>();

            (await Context.Categories.Where(i => i.User.Equals(user)).ToListAsync()).ForEach(item =>
            {
                result.Add(new Category()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            });

            return result;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category result = null;

            var entity = await Context.Categories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.Category, Category>(entity);

            return result;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            Category result = null;

            var entity = await Context.Categories.FirstOrDefaultAsync(i => i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (entity != null)
                result = Mapper.Map<Entity.Category, Category>(entity);

            return result;
        }

        public async Task<int> CreateCategory(int user, string name, string description)
        {
            var result = 0;

            var entity = new Entity.Category()
            {
                User = user,
                Name = name,
                Description = description
            };

            Context.Categories.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateCategory(int id, string name, string description)
        {
            var result = false;

            var entity = await Context.Categories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.Name = name;
                entity.Description = description;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var result = false;

            var entity = await Context.Categories.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.Categories.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}