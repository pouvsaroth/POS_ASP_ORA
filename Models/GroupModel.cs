using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("GROUP_TBL")]
    public class GroupModel
    {
        [Key]
        [Column("ID")]
        public int GroupId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("GROUPNAME")]
        public string GroupName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("REMARK")]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("COMPANYID")]
        public int CompanyId { get; set; }

        // Navigation property (Recommended)
        public Company? Company { get; set; }
    }
}
