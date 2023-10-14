using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
    {
        Task<Delivery?> GetByIdWithDeliveryAddressesAsync(int id);
    }
}
