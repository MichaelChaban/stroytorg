using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(c => c.Materials)
            .WithOne(m => m.Category)
            .HasForeignKey(m => m.CategoryId);
    }
}
