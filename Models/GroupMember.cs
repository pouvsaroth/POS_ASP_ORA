using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("GROUPMEMBER_TBL")]
    public class GroupMember
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("GROUPID")]
        public int GroupID { get; set; }

        [Required]
        [Column("USERID")]
        public int UserID { get; set; }
    }
}