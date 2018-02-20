using Microsoft.EntityFrameworkCore;
using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(DatabaseContext context)
                : base(context)
        {
        }

        public async Task<List<User>> GetUsers(int skip = 0, int count = 10)
        {
            var result = new List<User>();

            (await Context.Users.ToListAsync()).ForEach(item =>
            {
                result.Add(new User()
                {
                    ID = item.ID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.Phone
                });
            });

            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            User result = null;

            var entity = await Context.Users.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
                result = Mapper.Map<Entity.User, User>(entity);

            return result;
        }

        public async Task<int> CreateUser(string firstName, string lastName, string email, string phone)
        {
            var result = 0;

            var entity = new Entity.User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };

            Context.Users.Add(entity);

            if (await Context.SaveChangesAsync() > 0)
                result = entity.ID;

            return result;
        }

        public async Task<bool> UpdateUser(int id, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            var entity = await Context.Users.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                entity.FirstName = firstName;
                entity.LastName = lastName;
                entity.Email = email;
                entity.Phone = phone;

                Context.Update(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = false;

            var entity = await Context.Users.FirstOrDefaultAsync(i => i.ID.Equals(id));

            if (entity != null)
            {
                Context.Users.Remove(entity);
                result = await Context.SaveChangesAsync() > 0;
            }

            return result;
        }
    }
}