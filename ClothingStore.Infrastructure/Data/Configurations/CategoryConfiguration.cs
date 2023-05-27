using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.ID);
        builder
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Categories)
            .HasForeignKey(c => c.ParentCategoryID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.ParentCategoryID).IsRequired(false);
        builder.Property(c => c.IsActive).HasDefaultValue(true);
    }
}