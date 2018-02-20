using System.Collections.Generic;

namespace Mint.Database.Updater.Model
{
    public partial class TransactionType
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

        #region Navigation

        public ICollection<Transaction> Transaction { get; set; }

        #endregion

        #region .ctor

        public TransactionType()
        {
            Transaction = new HashSet<Transaction>();
        }

        #endregion
    }
}