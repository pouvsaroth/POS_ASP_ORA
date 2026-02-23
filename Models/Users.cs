using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_ASP_ORA.Models
{
    [Table("USERS_TBL")]
    public class Users
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("USERNAME")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [Column("PASSWORD")]
        [MaxLength(255)]
        public string Password { get; set; }

        [Column("EMAIL")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime? UpdatedAt { get; set; }
    }
}

