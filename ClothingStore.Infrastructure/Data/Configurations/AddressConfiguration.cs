using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.UserID);
        builder
            .HasOne(a => a.User)
            .WithOne(u => u.Address)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(a => a.Country).IsRequired();
        builder.Property(a => a.District).IsRequired().HasMaxLength(50);
        builder.Property(a => a.City).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Postcode).IsRequired().HasMaxLength(10);
        builder.Property(a => a.AddressLine1).IsRequired().HasMaxLength(50);
        builder.Property(a => a.AddressLine2).HasMaxLength(20);
        builder.Property(a => a.IsActive).HasDefaultValue(true);
    }
}