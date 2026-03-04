using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("IncomeType_tbl")]
    public class IncomeType
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("typename")]
        [MaxLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [Required]
        [Column("status")]
        public int Status { get; set; }
    }
}

