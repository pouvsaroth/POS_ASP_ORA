using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("PRODUCTUNITS_TBL")]
    public class ProductUnit
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("UNIT_NAME")]
        public string UnitName { get; set; } = string.Empty;
        public string UnitTypeName { get; set; } = string.Empty;

        [Column("UNITTYPE_ID")]
        public int? UnitTypeId { get; set; }

        [Column("QTY_PER_UNIT")]
        public decimal? QtyPerUnit { get; set; }

        [MaxLength(200)]
        [Column("REMARK")]
        public string? Remark { get; set; }

        [Column("STATUS")]
        public int? Status { get; set; }   // Oracle NUMBER
    }
}