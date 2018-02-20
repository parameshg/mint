using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Entity
{
    [Table("SubCategory")]
    public class SubCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("FK_SubCategory_User")]
        public int User { get; set; }

        [Required]
        [ForeignKey("FK_SubCategory_Category")]
        public int Category { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}