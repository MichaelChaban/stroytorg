using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Materials)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.OrderMaterialMap)
            .WithOne(oi => oi.Material)
            .HasForeignKey(oi => oi.MaterialId);
    }
}
