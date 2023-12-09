using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.OrderMaterialMap)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.ShippingAddress)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ShippingAddress);
    }
}
