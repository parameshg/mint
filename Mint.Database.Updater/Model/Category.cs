using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class Category
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User UserNavigation { get; set; }
        public ICollection<Transaction> Transaction { get; set; }

        public Category()
        {
            Transaction = new HashSet<Transaction>();
        }
    }
}