using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Configurations;

public class OrderMaterialMapConfiguration : IEntityTypeConfiguration<OrderMaterialMap>
{
    public void Configure(EntityTypeBuilder<OrderMaterialMap> builder)
    {
        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderMaterialMap)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(oi => oi.Material)
            .WithMany(m => m.OrderMaterialMap)
            .HasForeignKey(oi => oi.MaterialId);
    }
}
