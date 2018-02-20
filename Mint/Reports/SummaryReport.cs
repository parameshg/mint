namespace Mint.Reports
{
    public class SummaryReport
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public double Income { get; set; }

        public double Expense { get; set; }

        public double Value { get { return Income - Expense; } }
    }
}