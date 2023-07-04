using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ID);
        builder
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandID)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(p => p.SectionCategory)
            .WithMany(sc => sc.Products)
            .HasForeignKey(p => p.SectionCategoryID)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Price).IsRequired().HasColumnType("numeric");
        builder.Property(p => p.BrandID).IsRequired(false);
        builder.Property(p => p.AddDate).IsRequired();
        builder.Property(p => p.SectionCategoryID).IsRequired();
        builder.Property(p => p.InStockQuantity).IsRequired();
        builder.Property(p => p.ReservedQuantity).HasDefaultValue(0);
        builder.Property(p => p.ImageURL).IsRequired().HasMaxLength(500);
        builder.Property(p => p.IsActive).HasDefaultValue(true);
        
        builder
            .ToTable(p => p.HasCheckConstraint("price", "price > 0")
                .HasName("CK_product_price"));
        builder
            .ToTable(p => p.HasCheckConstraint("in_stock_quantity", "in_stock_quantity >= 0")
                .HasName("CK_product_in_stock_quantity"));
        builder
            .ToTable(p => p.HasCheckConstraint("reserved_quantity", "reserved_quantity >= 0")
                .HasName("CK_product_reserved_quantity"));
    }
}