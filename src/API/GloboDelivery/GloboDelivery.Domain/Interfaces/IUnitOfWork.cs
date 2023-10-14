using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAddressRepository AddressRepository { get; }
        IDeliveryRepository DeliveryRepository { get; }
        IBaseRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
