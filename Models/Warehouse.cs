using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("WAREHOUSE_TBL")]
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("WAREHOUSENAME")]
        public string Warehousename { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("LOCATION")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("PHONE")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("REMARK")]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("COMPANYID")]
        public int CompanyID { get; set; }
    }
}