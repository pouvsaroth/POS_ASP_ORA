using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("GROUND_TBL")]
    public class Group
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("GROUPNAME")]
        [MaxLength(100)]
        public string GroundName { get; set; } = string.Empty;

        [Column("REMARK")]
        [MaxLength(100)]
        public string? Remark { get; set; }

        [Required]
        [Column("COMPANYID")]
        public int CompanyId { get; set; }
    }
}
