using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            return services;
        }
    }
}
