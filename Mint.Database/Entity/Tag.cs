using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mint.Database.Entity
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("FK_Tags_User")]
        public int User { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}