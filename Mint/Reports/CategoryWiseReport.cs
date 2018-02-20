using System.Collections.Generic;

namespace Mint.Reports
{
    public class CategoryWiseReport
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public Dictionary<Category, double> Income { get; set; }

        public Dictionary<Category, double> Expense { get; set; }

        public CategoryWiseReport()
        {
            Income = new Dictionary<Category, double>();

            Expense = new Dictionary<Category, double>();
        }
    }
}