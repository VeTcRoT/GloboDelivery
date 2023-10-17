using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<T>> ListAllAsync();
        Task<PagedList<T>> ListPagedAsync(int pageNumber, int pageSize);
        Task CreateAsync(T entity);
        void Delete(T entity);
    }
}
