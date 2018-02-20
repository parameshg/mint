using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Entity
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("FK_Transaction_User")]
        public int User { get; set; }

        [Required]
        [ForeignKey("FK_Transaction_Account")]
        public int Account { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("FK_Transaction_Category")]
        public int Category { get; set; }

        [Required]
        [ForeignKey("FK_Transaction_SubCategory")]
        public int SubCategory { get; set; }

        [Required]
        [ForeignKey("FK_Transaction_TransactionType")]
        public int Type { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}