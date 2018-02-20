using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class User
    {
        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        #endregion

        #region Navigation

        public ICollection<Account> Account { get; set; }

        public ICollection<Category> Category { get; set; }

        public ICollection<SubCategory> SubCategory { get; set; }

        public ICollection<Tags> Tags { get; set; }

        public ICollection<Transaction> Transaction { get; set; }

        #endregion

        #region .ctor

        public User()
        {
            Account = new HashSet<Account>();

            Category = new HashSet<Category>();

            SubCategory = new HashSet<SubCategory>();

            Tags = new HashSet<Tags>();

            Transaction = new HashSet<Transaction>();
        }

        #endregion
    }
}