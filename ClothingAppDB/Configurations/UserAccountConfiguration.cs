using ClothingAppDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingAppDB.Configurations;

public class UserAccountConfiguration: IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.HasKey(u => u.ID);
        builder.Property(u => u.Phone).IsRequired(false).HasMaxLength(20);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(64);
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
        builder.HasIndex(u => u.Phone).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}