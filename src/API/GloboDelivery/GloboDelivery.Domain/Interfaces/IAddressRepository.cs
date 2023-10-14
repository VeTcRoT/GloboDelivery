using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<IReadOnlyList<Address>> GetAddressesByIdsAsync(IEnumerable<int> ids);
    }
}
