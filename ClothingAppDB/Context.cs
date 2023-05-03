using ClothingAppDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingAppDB;

public class Context : DbContext
{
    public Context()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=db;username=postgres;password=345510");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("clothing_store");

        // Table Section
        modelBuilder.Entity<Section>().HasKey(s => s.ID);
        modelBuilder.Entity<Section>().Property(s => s.Name).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Section>().HasIndex(s => s.Name).IsUnique();

        // Table Category
        modelBuilder.Entity<Category>().HasKey(c => c.ID);
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Categories)
            .HasForeignKey(c => c.ParentCategoryID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);

        // Table SectionCategory
        modelBuilder.Entity<SectionCategory>().HasKey(sc => sc.ID);
        modelBuilder.Entity<SectionCategory>()
            .HasOne(sc => sc.Section)
            .WithMany(s => s.SectionCategories)
            .HasForeignKey(sc => sc.SectionID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<SectionCategory>()
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SectionCategories)
            .HasForeignKey(sc => sc.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<SectionCategory>().Property(sc => sc.CategoryID).IsRequired();
        modelBuilder.Entity<SectionCategory>().Property(sc => sc.SectionID).IsRequired();

        // Table Brand
        modelBuilder.Entity<Brand>().HasKey(b => b.ID);
        modelBuilder.Entity<Brand>().Property(b => b.Name).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Brand>().HasIndex(b => b.Name).IsUnique();

        // Table Product
        modelBuilder.Entity<Product>().HasKey(p => p.ID);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.SectionCategory)
            .WithMany(sc => sc.Products)
            .HasForeignKey(p => p.SectionCategoryID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.BrandID).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.AddDate).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.SectionCategoryID).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.SectionCategoryID).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>().Property(p => p.Quantity).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.ImageURL).IsRequired().HasMaxLength(500);

        // Table UserAccount
        modelBuilder.Entity<UserAccount>().HasKey(u => u.ID);
        modelBuilder.Entity<UserAccount>().Property(u => u.Phone).HasMaxLength(20);
        modelBuilder.Entity<UserAccount>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<UserAccount>().Property(u => u.Password).IsRequired().HasMaxLength(64);
        modelBuilder.Entity<UserAccount>().Property(u => u.Role).IsRequired();
        modelBuilder.Entity<UserAccount>().Property(u => u.FirstName).HasMaxLength(50);
        modelBuilder.Entity<UserAccount>().Property(u => u.LastName).HasMaxLength(50);
        modelBuilder.Entity<UserAccount>().HasIndex(u => u.Phone).IsUnique();
        modelBuilder.Entity<UserAccount>().HasIndex(u => u.Email).IsUnique();

        // Table Address
        modelBuilder.Entity<Address>().HasKey(a => a.UserID);
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithOne(u => u.Address)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired();
        modelBuilder.Entity<Address>().Property(a => a.District).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(a => a.City).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(a => a.Postcode).IsRequired().HasMaxLength(10);
        modelBuilder.Entity<Address>().Property(a => a.AddressLine1).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(a => a.AddressLine2).HasMaxLength(20);

        // Table Review
        modelBuilder.Entity<Review>().HasKey(r => r.ID);
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Review>().Property(r => r.ProductID).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.UserID).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.ReviewTitle).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Review>().Property(r => r.Comment).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<Review>().Property(r => r.Rating).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.AddDate).IsRequired();

        // Table CustomerOrder
        modelBuilder.Entity<CustomerOrder>().HasKey(co => co.ID);
        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.User)
            .WithMany(u => u.CustomerOrders)
            .HasForeignKey(co => co.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CustomerOrder>().Property(co => co.UserID).IsRequired();
        modelBuilder.Entity<CustomerOrder>().Property(co => co.OrderDate).IsRequired();
        modelBuilder.Entity<CustomerOrder>().Property(co => co.CurrentStatus).IsRequired();

        // Table OrderProduct
        modelBuilder.Entity<OrderProduct>().HasKey(op => op.ID);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderProduct>().Property(op => op.ProductID).IsRequired();
        modelBuilder.Entity<OrderProduct>().Property(op => op.OrderID).IsRequired();
        modelBuilder.Entity<OrderProduct>().Property(op => op.Qauntity).IsRequired();
        modelBuilder.Entity<OrderProduct>().Property(op => op.Price).IsRequired();
        
        // Table OrderHistory
        modelBuilder.Entity<OrderHistory>().HasKey(oh => oh.ID);
        modelBuilder.Entity<OrderHistory>()
            .HasOne(oh => oh.CustomerOrder)
            .WithMany(o => o.OrderHistories)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<OrderHistory>().Property(oh => oh.OrderID).IsRequired();
        modelBuilder.Entity<OrderHistory>().Property(oh => oh.Status).IsRequired();
        modelBuilder.Entity<OrderHistory>().Property(oh => oh.Date).IsRequired();
    }

    public DbSet<Section> Sections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SectionCategory> SectionCategories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserAccount> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
}