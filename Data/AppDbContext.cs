using Microsoft.EntityFrameworkCore;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Users> UsersModel { get; set; }

        public DbSet<Customer> CustomerModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("RAW(16)")
                    .HasDefaultValueSql("SYS_GUID()");
                entity.Property(e => e.Username)
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Password)
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Email)
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.IsActive)
                    .HasColumnType("NUMBER(1)");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("SYSDATE");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER");

                entity.Property(e => e.CustomerName)
                      .HasColumnType("VARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Sex)
                      .HasColumnType("VARCHAR2(10)")
                      .IsRequired();

                entity.Property(e => e.Phone)
                      .HasColumnType("VARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnType("VARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasColumnType("VARCHAR2(150)")
                      .IsRequired();


                entity.Property(e => e.UserAccessId)
                      .HasColumnType("RAW(16)");

            });
        }
    }
}
