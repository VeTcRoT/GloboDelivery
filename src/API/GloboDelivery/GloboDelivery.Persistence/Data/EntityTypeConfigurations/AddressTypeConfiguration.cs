using GloboDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GloboDelivery.Persistence.Data.EntityTypeConfigurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.DeliveryAddresses)
                .WithOne(da => da.Address)
                .HasForeignKey(da => da.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
