using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
