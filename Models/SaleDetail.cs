using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("SALEDETAIL_TBL")]
    public class SaleDetail
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("SALEID")]
        public int SaleId { get; set; }

        [Required]
        [Column("PRODUCTID")]
        public int ProductId { get; set; }

        [Required]
        [Column("QTY")]
        public decimal Qty { get; set; }

        [Required]
        [Column("COST")]
        public decimal Cost { get; set; }

        [Required]
        [Column("PRICE")]
        public decimal Price { get; set; }

        [Required]
        [Column("SUBDISCOUNT")]
        public decimal SubDiscount { get; set; }
    }
}