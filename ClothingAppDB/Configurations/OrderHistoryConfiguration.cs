using ClothingAppDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingAppDB.Configurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.HasKey(oh => oh.ID);
        builder
            .HasOne(oh => oh.CustomerOrder)
            .WithMany(o => o.OrderHistories)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(oh => oh.OrderID).IsRequired();
        builder.Property(oh => oh.Status).IsRequired();
        builder.Property(oh => oh.Date).IsRequired();
    }
}