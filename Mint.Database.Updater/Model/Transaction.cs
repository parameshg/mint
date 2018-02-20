using System;
using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Account { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }

        public Category CategoryNavigation { get; set; }
        public Account AccountNavigation { get; set; }
        public SubCategory SubCategoryNavigation { get; set; }
        public TransactionType TypeNavigation { get; set; }
        public User UserNavigation { get; set; }
        public ICollection<TransactionTags> TransactionTags { get; set; }

        public Transaction()
        {
            TransactionTags = new HashSet<TransactionTags>();
        }
    }
}