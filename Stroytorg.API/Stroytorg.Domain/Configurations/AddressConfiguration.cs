using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId);

        builder.HasMany(a => a.Orders)
            .WithOne(u => u.ShippingAddress)
            .HasForeignKey(a => a.ShippingAddress);
    }
}
