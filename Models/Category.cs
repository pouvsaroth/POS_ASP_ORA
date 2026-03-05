using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("CATEGORY_TBL")]
    public class Category
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CATEGORYNAME")]
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [Column("STATUS")]
        public int Status { get; set; } 
    }
}
