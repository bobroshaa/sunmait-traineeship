﻿using ClothingStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Configurations;

public class BrandConfiguration: IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(b => b.ID);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(b => b.Name).IsUnique();
        builder.Property(b => b.IsActive).HasDefaultValue(true);
    }
}