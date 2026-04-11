using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("PURCHASE_TBL")]
    public class PurchaseModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("BILLNO")]
        [MaxLength(50)]
        public string BillNo { get; set; } = string.Empty;

        [Required]
        [Column("PURCHASEDATE")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Column("SUPPLIERID")]
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        [Required]
        [Column("TOTALAMOUNT", TypeName = "NUMBER(18,6)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("STATUS")]
        public int Status { get; set; } // 0=inactive, 1=active

        public decimal Paid { get; set; }
        public List<PurchaseDetailModel> Items { get; set; }
    }
    public class PurchaseDetailModel
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Qty { get; set; }

        public decimal Cost { get; set; }

        public decimal Total => Qty * Cost;
        public int ProductUnitId { get; set; }
        public int UnitId { get; set; }
        public decimal Vat { get; set; }
        public int CurrencyId { get; set; }
        public decimal Discount { get; set; }




    }
}