using System;
using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class Tags
    {
        public Tags()
        {
            TransactionTags = new HashSet<TransactionTags>();
        }

        public int Id { get; set; }
        public int User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User UserNavigation { get; set; }
        public ICollection<TransactionTags> TransactionTags { get; set; }
    }
}
