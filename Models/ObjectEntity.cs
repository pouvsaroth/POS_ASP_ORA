using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("OBJECT_TBL")]
    public class ObjectEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("OBJECTNAME")]
        public string ObjectName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;
    }
}