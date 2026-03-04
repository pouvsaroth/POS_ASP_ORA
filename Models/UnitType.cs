using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("UNITTYPE_TBL")]
    public class UnitType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("UNITTYPENAME")]
        [MaxLength(50)]
        public string UnitTypeName { get; set; } = string.Empty;

        [Required]
        [Column("STATUS")]
        public int Status { get; set; }  
    }
}
