using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("CustomerCheckin_tbl")]
    public class CustomerCheckin
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [Required]
        [Column("CheckinDate")]
        public DateTime CheckinDate { get; set; }

        [Required]
        [Column("status")]
        public int Status { get; set; }
    }
}