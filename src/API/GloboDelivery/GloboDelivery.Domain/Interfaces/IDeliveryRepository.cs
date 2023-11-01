using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
    {
        Task<Delivery?> GetByIdWithDeliveryAddressesAsync(int id);
        Task<PagedList<DeliveryAddress>?> GetPagedDeliveryAddressesAsync(int id, int pageNumber, int pageSize);
        Task<PagedList<Delivery>?> GetPagedDeliveryAddressesByConditionAsync(
            int pageNumber, int pageSize, decimal minCapacity, DateTime departureDate, DateTime arrivalDate, 
            string departureCountry, string departureCity, string arrivalCountry, string arrivalCity);
        Task<VanInfo?> GetDeliveryVanInfoAsync(int id);
    }
}
