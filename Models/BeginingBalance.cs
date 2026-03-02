using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("BEGININGBALANCE_TBL")]
    public class BeginingBalance
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("BALANCE_DATE")]
        public DateTime BalanceDate { get; set; }

        [Required]
        [Column("TOACCOUNTID")]
        public int ToAccountId { get; set; }

        [Required]
        [Column("AMOUNT")]
        public decimal Amount { get; set; }

        [Required]
        [Column("REMARK")]
        [MaxLength(100)]
        public string Remark { get; set; } = string.Empty;

        [Required]
        [Column("USERACCESSID")]
        public int UserAccessId { get; set; }
    }
}