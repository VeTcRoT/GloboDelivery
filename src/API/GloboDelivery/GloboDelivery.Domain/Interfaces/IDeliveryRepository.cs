using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
    {
        Task<Delivery?> GetByIdWithDeliveryAddressesAsync(int id);
        Task<PagedList<Address>?> GetPagedDeliveryAddressesAsync(int id, int pageNumber, int pageSize);
        Task<VanInfo?> GetDeliveryVanInfoAsync(int id);
    }
}
