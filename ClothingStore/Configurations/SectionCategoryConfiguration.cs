using ClothingStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Configurations;

public class SectionCategoryConfiguration : IEntityTypeConfiguration<SectionCategory>
{
    public void Configure(EntityTypeBuilder<SectionCategory> builder)
    {
        builder.HasKey(sc => sc.ID);
        builder
            .HasOne(sc => sc.Section)
            .WithMany(s => s.SectionCategories)
            .HasForeignKey(sc => sc.SectionID)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SectionCategories)
            .HasForeignKey(sc => sc.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(sc => sc.CategoryID).IsRequired();
        builder.Property(sc => sc.SectionID).IsRequired();
    }
}