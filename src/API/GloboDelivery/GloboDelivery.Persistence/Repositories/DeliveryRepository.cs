using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
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

        public async Task<PagedList<DeliveryAddress>?> GetPagedDeliveryAddressesAsync(int id, int pageNumber, int pageSize)
        {
            var addresses = _dbSet.Where(d => d.Id == id).SelectMany(d => d.DeliveryAddresses).Include(da => da.Address).Select(da => da);
            return await PagedList<DeliveryAddress>.CreateAsync(addresses, pageNumber, pageSize);
        }

        public async Task<VanInfo?> GetDeliveryVanInfoAsync(int id)
        {
            return await _dbSet.Where(d => d.Id == id).Include(d => d.VanInfo).Select(d => d.VanInfo).FirstOrDefaultAsync();
        }
    }
}
