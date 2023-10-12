using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<T>> ListAllAsync();
        void CreateAsync(T entity);
        void Delete(T entity);
    }
}
