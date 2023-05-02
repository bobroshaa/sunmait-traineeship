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
        
        modelBuilder.Entity<Section>().HasKey(s => s.ID);

        modelBuilder.Entity<Category>().HasKey(c => c.ID);
        
        modelBuilder.Entity<SectionCategory>().HasKey(sc => sc.ID);
        modelBuilder.Entity<SectionCategory>()
            .HasOne(sc => sc.Section)
            .WithMany(s => s.SectionCategories)
            .HasForeignKey(sc => sc.SectionID);
        modelBuilder.Entity<SectionCategory>()
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SectionCategories)
            .HasForeignKey(sc => sc.CategoryID);
        
        modelBuilder.Entity<Brand>().HasKey(b => b.ID);
        
        modelBuilder.Entity<Product>().HasKey(p => p.ID);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandID);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Section)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SectionID);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.CategoryID);
        
        modelBuilder.Entity<UserAccount>().HasKey(u => u.ID);
        
        modelBuilder.Entity<Address>().HasKey(a => a.UserID);
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithOne(u => u.Address);

        modelBuilder.Entity<Review>().HasKey(r => r.ID);
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductID);
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserID);
        
        modelBuilder.Entity<CustomerOrder>().HasKey(o => o.ID);
        modelBuilder.Entity<CustomerOrder>()
            .HasOne(o => o.User)
            .WithMany(u => u.CustomerOrders)
            .HasForeignKey(o => o.UserID);
        
        modelBuilder.Entity<OrderProduct>().HasKey(op => op.ID);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductID);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderID);
        
        modelBuilder.Entity<OrderHistory>().HasKey(oh => oh.ID);
        modelBuilder.Entity<OrderHistory>()
            .HasOne(oh => oh.CustomerOrder)
            .WithMany(o => o.OrderHistories)
            .HasForeignKey(op => op.OrderID);
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