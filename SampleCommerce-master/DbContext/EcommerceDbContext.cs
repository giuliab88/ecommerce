using Microsoft.EntityFrameworkCore;
using SampleCommerce.Models;

namespace SampleCommerce.Context { 
    public partial class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext()
        {
        }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<StockKeepingUnit> StockKeepingUnits { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserAddress> UserAddresses { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SampleEcommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC07AA498251");
                entity.HasQueryFilter(e => e.IsActive);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.ComplementAddress).HasMaxLength(250);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Province).HasMaxLength(100);
                entity.Property(e => e.Receiver).HasMaxLength(100);
                entity.Property(e => e.StreetAdress).HasMaxLength(250);
                entity.Property(e => e.ZipCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC078C2E4871");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__AddressI__6A30C649");

                entity.HasOne(d => d.User).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__UserId__693CA210");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.SkuId }).HasName("PK__OrderIte__897D377225D46D74");

                entity.Property(e => e.MomentPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__6D0D32F4");

                entity.HasOne(d => d.Sku).WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.SkuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__SkuId__6E01572D");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Products__3214EC070FB9337F");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Brand).HasMaxLength(50);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Seller__5AEE82B9");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07758DE388");

                entity.HasIndex(e => new { e.UserId, e.ProductId }, "UC_User_Product").IsUnique();

                entity.Property(e => e.Comment).HasMaxLength(100);
                entity.Property(e => e.ReviewDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__Product__6477ECF3");

                entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__UserId__656C112C");
            });

            modelBuilder.Entity<StockKeepingUnit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__StockKee__3214EC07BC5DC9E8");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product).WithMany(p => p.StockKeepingUnits)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockKeep__Produ__5EBF139D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E97C0FFA");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534926FEC68").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Iva).HasMaxLength(11);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.TradingName).HasMaxLength(100);
                entity.Property(e => e.EmailConfirmed).HasDefaultValue(false);
                entity.Property(e => e.EmailConfirmationToken).HasMaxLength(128);
                entity.Property(e => e.PasswordResetToken).HasMaxLength(128);
                entity.Property(e => e.PasswordResetTokenExpiry).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AddressId }).HasName("PK__UserAddr__B7190EE3D7834F7C");

                entity.HasIndex(e => e.UserId, "UX_UserAddress_OnePreferredPerUser")
                    .IsUnique()
                    .HasFilter("([IsPreferred]=(1))");

                entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAddre__Addre__5441852A");

                entity.HasOne(d => d.User).WithOne(p => p.UserAddress)
                    .HasForeignKey<UserAddress>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAddre__UserI__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}