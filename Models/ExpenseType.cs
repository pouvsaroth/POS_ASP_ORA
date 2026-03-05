using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("EXPENSETYPE_TBL")]
    public class ExpenseType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TYPENAME")]
        [MaxLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [Required]
        [Column("STATUS")]
        public int Status { get; set; }  
    }
}