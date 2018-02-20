using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Updater.Model
{
    [Table("Account")]
    public partial class Account
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public int User { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

        #region Navigation

        public User UserNavigation { get; set; }

        public Transaction Transaction { get; set; }

        #endregion
    }
}