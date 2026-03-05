
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("PURCHASEPAYMENT_TBL")]
    public class PurchasePayment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("PURCHASEID")]
        public int PurchaseId { get; set; }

        [Required]
        [Column("PAYMENTDATE")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Column("PAYMENTMETHOD")]
        public int PaymentMethod { get; set; }

        [Required]
        [Column("PAYAMOUNT", TypeName = "NUMBER(18,6)")]
        public decimal PayAmount { get; set; }
    }
}