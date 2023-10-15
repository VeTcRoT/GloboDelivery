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

            builder.Property(vi => vi.Mark).HasMaxLength(20).IsRequired();
            builder.Property(vi => vi.Model).HasMaxLength(20).IsRequired();
            builder.Property(vi => vi.Color).HasMaxLength(15).IsRequired();
            builder.Property(vi => vi.Remarks).HasMaxLength(250);
            builder.Property(vi => vi.Capacity).IsRequired();
            builder.Property(vi => vi.Year).IsRequired();
            builder.Property(vi => vi.LastInspectionDate).IsRequired();

            builder.HasMany(vi => vi.Deliveries)
                .WithOne(d => d.VanInfo)
                .HasForeignKey(d => d.VanInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
