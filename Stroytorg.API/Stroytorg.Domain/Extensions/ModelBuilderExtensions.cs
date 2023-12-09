using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Configurations;

namespace Stroytorg.Domain.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyStroytorgConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new AddressConfiguration())
            .ApplyConfiguration(new CategoryConfiguration())
            .ApplyConfiguration(new MaterialConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new OrderMaterialMapConfiguration());
    }
}
