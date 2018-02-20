using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class TagRepository : Repository, ITagRepository
    {
        public TagRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<Tag>> GetTags(int user)
        {
            var result = new List<Tag>();

            (await Context.Tags.Where(i => i.User.Equals(user)).ToListAsync()).ForEach(item =>
            {
                result.Add(new Tag()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description
                });
            });

            return result;
        }

        public async Task<Tag> GetTagById(int id)
        {
            Tag result = null;

            var entity = await Context.Tags.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.Tag, Tag>(entity);

            return result;
        }

        public async Task<int> CreateTag(int user, string name, string description)
        {
            var result = 0;

            var entity = new Entity.Tag()
            {
                User = user,
                Name = name,
                Description = description
            };

            Context.Tags.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateTag(int id, string name, string description)
        {
            var result = false;

            var entity = await Context.Tags.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.Name = name;
                entity.Description = description;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteTag(int id)
        {
            var result = false;

            var entity = await Context.Tags.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.Tags.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}