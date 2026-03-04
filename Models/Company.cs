using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("COMPANY_TBL")]
    public class Company
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("COMPANYNAME")]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Column("LOCATION")]
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Column("PHONE")]
        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Column("REMARK")]
        [MaxLength(50)]
        public string Remark { get; set; } = string.Empty;
    }
}
