using ClothingAppDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingAppDB.Configurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(op => op.ID);
        builder
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(op => op.ProductID).IsRequired();
        builder.Property(op => op.OrderID).IsRequired();
        builder.Property(op => op.Quantity).IsRequired();
        builder.Property(op => op.Price).IsRequired().HasColumnType("numeric");
        builder
            .ToTable(op => op.HasCheckConstraint("quantity", "quantity > 0")
                .HasName("CK_order_product_quantity"));
        builder
            .ToTable(op => op.HasCheckConstraint("price", "price > 0")
                .HasName("CK_order_product_price"));
    }
}