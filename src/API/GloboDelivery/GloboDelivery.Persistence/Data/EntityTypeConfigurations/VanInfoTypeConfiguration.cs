using GloboDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.Persistence.Data.EntityTypeConfigurations
{
    public class VanInfoTypeConfiguration : IEntityTypeConfiguration<VanInfo>
    {
        public void Configure(EntityTypeBuilder<VanInfo> builder)
        {
            builder.HasKey(vi => vi.Id);

            builder.HasMany(vi => vi.Deliveries)
                .WithOne(d => d.VanInfo)
                .HasForeignKey(d => d.VanInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
