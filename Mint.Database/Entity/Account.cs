using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Entity
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("FK_Account_User")]
        public int User { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}