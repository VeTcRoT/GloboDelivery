using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.Persistence.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(GloboDeliveryDbContext dbContext) : base(dbContext) { }

        public async Task<Delivery?> GetByIdWithDeliveryAddressesAsync(int id)
        {
            return await _dbSet.Include(d => d.DeliveryAddresses).Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Address>?> GetDeliveryAddressesAsync(int id)
        {
            var deliveries = await _dbSet.Where(d => d.Id == id).Include(d => d.DeliveryAddresses).ThenInclude(da => da.Address).FirstOrDefaultAsync();
            return deliveries?.DeliveryAddresses.Select(da => da.Address).ToList();
        }

        public async Task<VanInfo?> GetDeliveryVanInfoAsync(int id)
        {
            return await _dbSet.Where(d => d.Id == id).Include(d => d.VanInfo).Select(d => d.VanInfo).FirstOrDefaultAsync();
        }
    }
}
