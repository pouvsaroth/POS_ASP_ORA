using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("SUPPLIER_TBL")]
    public class Supplier
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("SUPPLIERNAME")]
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("SEX")]
        public string Sex { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("PHONE")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("ADDRESS")]
        public string Address { get; set; } = string.Empty;
    }
}