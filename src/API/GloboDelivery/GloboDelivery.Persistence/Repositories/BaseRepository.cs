using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(GloboDeliveryDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PagedList<T>> ListPagedAsync(int pageNumber, int pageSize)
        {
            return await PagedList<T>.CreateAsync(_dbSet, pageNumber, pageSize);
        }
    }
}
