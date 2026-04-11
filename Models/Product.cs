using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("PRODUCT_TBL")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Column("PRODUCTCODE")]
        public string ProductCode { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Column("BARCODE")]
        public string Barcode { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Column("PRODUCTNAMEKH")]
        public string ProductNameKh { get; set; } = string.Empty;

        [Required]
        [Column("CATEGORYID")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [Column("SUPPLIERID")]
        public int SupplierId { get; set; }

        [Column("QTYONHAND")]
        public decimal? QtyOnHand { get; set; }

        [Column("QTYALERT")]
        public int? QtyAlert { get; set; }

        [Column("IMAGENAME")]
        public string ImageName { get; set; }

        [MaxLength(50)]
        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("STATUS")]
        public int Status { get; set; }

        [Column("USERACCESSID")]
        public int? UserAccessId { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string OldImageName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string Currency { get; set; } = "USD"; // Default currency, can be changed as needed
    }
}