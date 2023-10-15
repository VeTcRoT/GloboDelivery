using GloboDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.Persistence.Data.EntityTypeConfigurations
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Price).IsRequired();
            builder.Property(d => d.DepartureDate).IsRequired();
            builder.Property(d => d.ArrivalDate).IsRequired();
            builder.Property(d => d.IsAvailable).IsRequired();
            builder.Property(d => d.CapacityTaken).IsRequired();

            builder.HasMany(d => d.DeliveryAddresses)
                .WithOne(da => da.Delivery)
                .HasForeignKey(da => da.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
