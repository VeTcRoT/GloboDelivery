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
    }
}
