using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAddressRepository AddressRepository { get; }
        public IDeliveryRepository DeliveryRepository { get; }

        private readonly GloboDeliveryDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(
            IAddressRepository addressRepository, 
            IDeliveryRepository deliveryRepository, 
            GloboDeliveryDbContext dbContext, 
            IServiceProvider serviceProvider)
        {
            AddressRepository = addressRepository;
            DeliveryRepository = deliveryRepository;
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public IBaseRepository<T>? Repository<T>() where T : BaseEntity
        {
            return _serviceProvider.GetService<IBaseRepository<T>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
