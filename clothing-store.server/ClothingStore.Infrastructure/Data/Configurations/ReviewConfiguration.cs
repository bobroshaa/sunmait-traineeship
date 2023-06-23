using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.ID);
        builder
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(r => r.ProductID).IsRequired();
        builder.Property(r => r.UserID).IsRequired();
        builder.Property(r => r.ReviewTitle).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Comment).IsRequired().HasMaxLength(500);
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.AddDate).IsRequired();
        builder
            .ToTable(r => r.HasCheckConstraint("rating", "rating >= 0 AND rating <= 5")
                .HasName("CK_review_rating"));
        builder.Property(r => r.IsActive).HasDefaultValue(true);
    }
}