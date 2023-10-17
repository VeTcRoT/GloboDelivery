using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAddressRepository AddressRepository { get => _addressRepository.Value; }
        public IDeliveryRepository DeliveryRepository { get => _deliveryRepository.Value; }

        private readonly Lazy<IAddressRepository> _addressRepository;
        private readonly Lazy<IDeliveryRepository> _deliveryRepository;
        private readonly GloboDeliveryDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(
            Lazy<IAddressRepository> addressRepository, 
            Lazy<IDeliveryRepository> deliveryRepository, 
            GloboDeliveryDbContext dbContext, 
            IServiceProvider serviceProvider)
        {
            _addressRepository = addressRepository;
            _deliveryRepository = deliveryRepository;
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
