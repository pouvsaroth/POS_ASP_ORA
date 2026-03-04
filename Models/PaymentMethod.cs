using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("PAYMENTMETHOD_TBL")]
    public class PaymentMethod
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("METHODNAME")]
        [MaxLength(50)]
        public string MethodName { get; set; } = string.Empty;

        [Required]
        [Column("STATUS")]
        public int Status { get; set; }  
    }
}
