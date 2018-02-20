using Microsoft.EntityFrameworkCore;

namespace Mint.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Entity.Account> Accounts { get; set; }

        public DbSet<Entity.Category> Categories { get; set; }

        public DbSet<Entity.SubCategory> SubCategories { get; set; }

        public DbSet<Entity.Tag> Tags { get; set; }

        public DbSet<Entity.Transaction> Transactions { get; set; }

        public DbSet<Entity.TransactionType> TransactionTypes { get; set; }

        public DbSet<Entity.TransactionTag> TransactionTags { get; set; }

        public DbSet<Entity.User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }
}