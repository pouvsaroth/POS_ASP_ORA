using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("SALE_TBL")]
    public class Sale
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("INVOICENO")]
        public int InvoiceNo { get; set; }

        [Required]
        [Column("SALEDATE")]
        public DateTime SaleDate { get; set; }

        [Required]
        [Column("CUSTOMERID")]
        public int CustomerId { get; set; }

        [Required]
        [Column("TOTALAMOUNT")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("DISCOUNT")]
        public decimal Discount { get; set; }

        [Required]
        [Column("STATUS")]
        public int Status { get; set; }

        [Column("USERACCESSID")]
        public int? UserAccessID { get; set; }
    }
}
