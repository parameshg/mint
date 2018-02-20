using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Entity
{
    [Table("TransactionTag")]
    public class TransactionTag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Transaction { get; set; }

        [Required]
        public int Tag { get; set; }
    }
}