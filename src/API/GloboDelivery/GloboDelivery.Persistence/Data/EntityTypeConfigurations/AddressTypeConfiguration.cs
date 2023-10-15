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

            builder.Property(a => a.Country).HasMaxLength(20).IsRequired();
            builder.Property(a => a.City).HasMaxLength(20).IsRequired();
            builder.Property(a => a.AdministrativeArea).HasMaxLength(20).IsRequired();
            builder.Property(a => a.AddressLine).HasMaxLength(45).IsRequired();
            builder.Property(a => a.PostalCode).HasMaxLength(25).IsRequired();
            builder.Property(a => a.SuiteNumber).IsRequired();

            builder.HasMany(a => a.DeliveryAddresses)
                .WithOne(da => da.Address)
                .HasForeignKey(da => da.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
