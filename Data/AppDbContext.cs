using Microsoft.EntityFrameworkCore;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Users> UsersModel { get; set; }
        public DbSet<CashTransfer> CashTransferModel { get; set; }
        public DbSet<Category> CategoryModel { get; set; }
        public DbSet<Company> CompanyModel { get; set; }
        public DbSet<CustomerCheckin> CustomerCheckinModel { get; set; }
        public DbSet<IncomeType> IncomeTypeModel { get; set; }
        public DbSet<MoreCapital> MoreCapitalModel { get; set; }
        public DbSet<Income> IncomeModel { get; set; }
        public DbSet<OwnerDrawing> OwnerDrawingModel { get; set; }
        public DbSet<ExpenseType> ExpenseTypeModel { get; set; }
        public DbSet<PurchasePayment> PurchasePaymentModel { get; set; }
        public DbSet<Purchase> PurchaseModel { get; set; }
        public DbSet<PaymentMethod> PaymentMethodModel { get; set; }
        public DbSet<UnitType> UnitTypeModel { get; set; }
        public DbSet<SalePayment> SalePaymentModel { get; set; }
        public DbSet<SaleDetail> SaleDetailModel { get; set; }
        public DbSet<Sale> SaleModel { get; set; }
        public DbSet<ObjectEntity> ObjectModel { get; set; }
        public DbSet<GroupObject> GroupObjectModel { get; set; }
        public DbSet<GroupMember> GroupMemberModel { get; set; }
        public DbSet<Supplier> SupplierModel { get; set; }
        public DbSet<Group> GroupModel { get; set; }
        public DbSet<Product> ProductModel { get; set; }
        public DbSet<Warehouse> WarehouseModel { get; set; }

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

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .UseIdentityColumn();

                entity.Property(e => e.Warehousename)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Location)
                      .HasColumnType("VARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Phone)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.CompanyID)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductCode)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Barcode)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.ProductName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.ProductNameKh)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.CategoryId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.SupplierId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.QtyOnHand)
                      .HasColumnType("NUMBER(18,6)");

                entity.Property(e => e.QtyAlert)
                      .HasColumnType("NUMBER");

                entity.Property(e => e.Image)
                      .HasColumnType("BLOB");

                entity.Property(e => e.Description)
                      .HasColumnType("NVARCHAR2(50)");

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .HasConversion<int?>();  // ✅ Conversion fixes Oracle bool mapping

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NUMBER");

                // Foreign keys
                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supplier)
                      .WithMany()
                      .HasForeignKey(p => p.SupplierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.GroupName)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.CompanyID)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                // Foreign Key Relationship
                entity.HasOne(g => g.Company)
                      .WithMany()
                      .HasForeignKey(g => g.CompanyID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.GroupID)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.UserID)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.SupplierName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Sex)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Phone)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();
            });

            modelBuilder.Entity<GroupObject>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .UseIdentityColumn();

                entity.Property(e => e.GroupID)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.ObjectID)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<ObjectEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .UseIdentityColumn();

                entity.Property(e => e.ObjectName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .UseIdentityColumn();

                entity.Property(e => e.InvoiceNo)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.SaleDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.CustomerId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.TotalAmount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Discount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();

                entity.Property(e => e.UserAccessID)
                      .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .UseIdentityColumn();

                entity.Property(e => e.SaleId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.ProductId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.Qty)
                      .HasColumnType("NUMBER(18,3)")
                      .IsRequired();

                entity.Property(e => e.Cost)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Price)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.SubDiscount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();
            });

            modelBuilder.Entity<CashTransfer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.TransferDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.FromAccountId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.ToAccountId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .HasConversion<int>()
                      .IsRequired();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyName)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Location)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Phone)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();
            });

            modelBuilder.Entity<CustomerCheckin>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.CheckinDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<IncomeType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.TypeName)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<MoreCapital>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.MoreDate)
                      .HasColumnType("DATE")
                      .IsRequired();

                entity.Property(e => e.ToAccountId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ComeDate)
                      .HasColumnType("DATE")
                      .IsRequired();

                entity.Property(e => e.PaymentMethodId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnType("DECIMAL(18,2)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.IncomeTypeId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<OwnerDrawing>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.DrawingDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.FromAccountId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.DrawingAmount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Remark)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NUMBER")
                      .IsRequired();
            });

            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.TypeName)
                      .HasColumnType("NVARCHAR2(100)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<PurchasePayment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.PurchaseId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.PaymentDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.PaymentMethod)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.PayAmount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.BillNo)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.PurchaseDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.SupplierId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.TotalAmount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Discount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();

                entity.Property(e => e.UserAccessId)
                      .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.MethodName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<UnitType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UnitTypeName)
                      .HasColumnType("NVARCHAR2(50)")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnType("NUMBER(1)")
                      .IsRequired();
            });

            modelBuilder.Entity<SalePayment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnType("NUMBER")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.SaleId)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.PaymentDate)
                      .HasColumnType("TIMESTAMP")
                      .IsRequired();

                entity.Property(e => e.PaymentMethod)
                      .HasColumnType("NUMBER")
                      .IsRequired();

                entity.Property(e => e.PayAmount)
                      .HasColumnType("NUMBER(18,6)")
                      .IsRequired();
            });
        }
    }
}
