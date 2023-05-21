using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomerOrderConfiguration: IEntityTypeConfiguration<CustomerOrder>
{
    public void Configure(EntityTypeBuilder<CustomerOrder> builder)
    {
        builder.HasKey(co => co.ID);
        builder
            .HasOne(co => co.User)
            .WithMany(u => u.CustomerOrders)
            .HasForeignKey(co => co.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(co => co.UserID).IsRequired();
        builder.Property(co => co.OrderDate).IsRequired();
        builder.Property(co => co.CurrentStatus).IsRequired();
    }
}