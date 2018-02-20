using System;

namespace Mint
{
    public class Transaction
    {
        public int ID { get; set; }

        public Account Account { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }

        public TransactionType Type { get; set; }

        public double Amount { get; set; }
    }
}