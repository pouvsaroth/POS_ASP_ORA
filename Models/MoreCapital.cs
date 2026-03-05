using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("MoreCapital_tbl")]
    public class MoreCapital
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("moredate")]
        public DateTime MoreDate { get; set; }

        [Required]
        [Column("toaccountid")]
        public int ToAccountId { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("remark")]
        [MaxLength(100)]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("useraccessid")]
        [MaxLength(50)]
        public string UserAccessId { get; set; } = string.Empty;
    }
}
