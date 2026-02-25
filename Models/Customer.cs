using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("CUSTOMER_TBL")]
    public class Customer
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CUSTOMERNAME")]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [Column("SEX")]
        [MaxLength(10)]
        public string Sex { get; set; }

        [Required]
        [Column("PHONE")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column("EMAIL")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("ADDRESS")]
        [MaxLength(150)]
        public string Address { get; set; }

        [Column("USERACCESSID")]
        public string? UserAccessId { get; set; }

    }
}
