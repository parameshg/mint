using System;
using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int User { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User UserNavigation { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
