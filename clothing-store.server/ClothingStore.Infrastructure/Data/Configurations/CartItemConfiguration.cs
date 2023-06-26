using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.ID);
        builder
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(ci => ci.ProductID).IsRequired();
        builder.Property(ci => ci.UserID).IsRequired();
        builder.Property(ci => ci.Quantity).IsRequired();
        builder.Property(ci => ci.ReservationEndDate).IsRequired();
        builder
            .ToTable(op => op.HasCheckConstraint("quantity", "quantity > 0")
                .HasName("CK_order_product_quantity"));
        builder.Property(b => b.IsActive).HasDefaultValue(true);
    }
}