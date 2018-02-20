using Mint.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mint.Database.Repositories
{
    public class SeedRepository : Repository, ISeedRepository
    {
        private Dictionary<string, List<string>> Categories { get; set; }

        private List<string> Types { get; set; }

        private List<string> Tags { get; set; }

        public SeedRepository(DatabaseContext context)
            : base(context)
        {
            Categories = new Dictionary<string, List<string>>();
            Categories.Add("Home", new List<string>(new string[] { "Home", "Rent", "Groceries", "Shopping" }));
            Categories.Add("Utilities", new List<string>(new string[] { "Power", "Water", "Internet", "Phone", "TV" }));
            Categories.Add("Travel", new List<string>(new string[] { "Car", "Bus", "Train", "Flight" }));
            Categories.Add("Entertainment", new List<string>(new string[] { "Movies", "Resturant", "Drinks" }));
            Categories.Add("Tax", new List<string>(new string[] { "Tax", "Tax Agent", "Tax Refund" }));
            Categories.Add("Personal", new List<string>(new string[] { "Personal", "Health", "Misc" }));
            Categories.Add("Kids", new List<string>(new string[] { "Toys", "School", "Health" }));

            Types = new List<string>();
            Types.Add("Income");
            Types.Add("Expense");

            Tags = new List<string>();
            Tags.Add("Investments");
            Tags.Add("Capital");
        }

        public async Task<bool> Install()
        {
            var result = false;

            foreach (var i in Types)
            {
                await Context.TransactionTypes.AddAsync(new Entity.TransactionType()
                {
                    Name = i,
                    Description = i
                });
            }

            await Context.Users.AddAsync(new Entity.User()
            {
                FirstName = "demo",
                LastName = "demo",
                Email = "demo@localhost",
                Phone = "000-000-0000"
            });

            result = await Context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> Populate(int user)
        {
            var result = false;

            foreach (var i in Categories.Keys)
            {
                var category = new Entity.Category()
                {
                    User = user,
                    Name = i,
                    Description = i
                };

                await Context.Categories.AddAsync(category);

                foreach (var j in Categories[i])
                {
                    await Context.SubCategories.AddAsync(new Entity.SubCategory()
                    {
                        User = user,
                        Category = category.ID,
                        Name = j,
                        Description = j
                    });
                }
            }

            foreach (var tag in Tags)
            {
                await Context.Tags.AddAsync(new Entity.Tag()
                {
                    User = user,
                    Name = tag,
                    Description = tag
                });
            }

            result = await Context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> Uninstall()
        {
            var result = false;

            Context.TransactionTags.RemoveRange(Context.TransactionTags.ToArray());
            Context.Transactions.RemoveRange(Context.Transactions.ToArray());
            Context.TransactionTypes.RemoveRange(Context.TransactionTypes.ToArray());
            Context.Accounts.RemoveRange(Context.Accounts.ToArray());
            Context.Categories.RemoveRange(Context.Categories.ToArray());
            Context.SubCategories.RemoveRange(Context.SubCategories.ToArray());
            Context.Tags.RemoveRange(Context.Tags.ToArray());

            await Context.SaveChangesAsync();

            return result;
        }
    }
}