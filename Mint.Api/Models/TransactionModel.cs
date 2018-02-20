using System;

namespace Mint.Api.Models
{
    public class CreateTransactionModel
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Category { get; set; }

        public int SubCategory { get; set; }

        public int Type { get; set; }

        public double Amount { get; set; }
    }

    public class UpdateTransactionModel
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Category { get; set; }

        public int SubCategory { get; set; }

        public int Type { get; set; }

        public double Amount { get; set; }
    }
}