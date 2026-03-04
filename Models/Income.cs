using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("INCOME_TBL")]
    public class Income
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("COMEDATE")]
        public DateTime ComeDate { get; set; }

        [Required]
        [Column("PAYMENTMETHODID")]
        public int PaymentMethodId { get; set; }

        [Required]
        [Column("AMOUNT", TypeName = "DECIMAL(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column("REMARK")]
        [MaxLength(100)]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("INCOMETYPEID")]
        public int IncomeTypeId { get; set; }

        [Required]
        [Column("USERACCESSID")]
        public int UserAccessId { get; set; }
    }
}