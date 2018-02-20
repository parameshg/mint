namespace Mint
{
    public enum TransactionTypes
    {
        Income,
        Expense
    }

    public class TransactionType
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}