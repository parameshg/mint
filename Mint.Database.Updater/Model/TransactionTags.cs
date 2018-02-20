namespace Mint.Database.Updater.Model
{
    public partial class TransactionTags
    {
        public int Id { get; set; }

        public int Transaction { get; set; }

        public int Tag { get; set; }

        public Tags TagNavigation { get; set; }

        public Transaction TransactionNavigation { get; set; }
    }
}