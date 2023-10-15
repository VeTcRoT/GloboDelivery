using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.Persistence.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(GloboDeliveryDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Address>> GetAddressesByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet.Where(address => ids.Contains(address.Id)).ToListAsync();
        }
    }
}