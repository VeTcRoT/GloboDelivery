using GloboDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GloboDelivery.Persistence.Data
{
    public class GloboDeliveryDbContext : DbContext
    {
        public DbSet<VanInfo> VanInfos { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Delivery> Deliveries { get; set; } = null!;
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;

        public GloboDeliveryDbContext(DbContextOptions<GloboDeliveryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
