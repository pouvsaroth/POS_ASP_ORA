using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("OWNERDRAWING_TBL")]
    public class OwnerDrawing
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("DRAWINGDATE")]
        public DateTime DrawingDate { get; set; }

        [Required]
        [Column("FROMACCOUNTID")]
        public int FromAccountId { get; set; }

        [Required]
        [Column("DRAWINGAMOUNT", TypeName = "NUMBER(18,6)")]
        public decimal DrawingAmount { get; set; }

        [Required]
        [Column("REMARK")]
        [MaxLength(100)]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("USERACCESSID")]
        public int UserAccessId { get; set; }
    }
}
