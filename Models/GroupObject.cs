using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("GROUPOBJECT_TBL")]
    public class GroupObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("GROUPID")]
        public int GroupID { get; set; }

        [Required]
        [Column("OBJECTID")]
        public int ObjectID { get; set; }
    }
}