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

        public async Task<PagedList<Delivery>?> GetPagedDeliveryAddressesByConditionAsync(int pageNumber, int pageSize, decimal minCapacity, DateTime departureDate, DateTime arrivalDate, string departureCountry, string departureCity, string arrivalCountry, string arrivalCity)
        {
            var deliveries = _dbSet
                .Include(d => d.VanInfo)
                .Include(d => d.DeliveryAddresses).ThenInclude(da => da.Address)
                .Where(d =>
                    d.VanInfo.Capacity - d.CapacityTaken >= minCapacity &&
                    d.DeliveryAddresses.Where(da =>
                        (da.DepartureDate.Date >= departureDate.Date && da.Address.Country == departureCountry && da.Address.City == departureCity) ||
                        (da.ArrivalDate.Date <= arrivalDate.Date && da.Address.Country == arrivalCountry && da.Address.City == arrivalCity)
                    ).Count() == 2
                );
             
            return await PagedList<Delivery>.CreateAsync(deliveries, pageNumber, pageSize);
        }

    }
}
