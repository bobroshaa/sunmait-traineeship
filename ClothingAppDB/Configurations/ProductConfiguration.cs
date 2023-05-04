using ClothingAppDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingAppDB.Configurations;

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
        builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasColumnType("numeric");
        builder.Property(p => p.BrandID).IsRequired();
        builder.Property(p => p.AddDate).IsRequired();
        builder.Property(p => p.SectionCategoryID).IsRequired();
        builder.Property(p => p.SectionCategoryID).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.ImageURL).IsRequired().HasMaxLength(500);
        builder
            .ToTable(p => p.HasCheckConstraint("Price", "\"Price\" > 0")
                .HasName("CK_Product_Price"));
        builder
            .ToTable(p => p.HasCheckConstraint("Quantity", "\"Quantity\" >= 0")
                .HasName("CK_Product_Quantity"));
    }
}