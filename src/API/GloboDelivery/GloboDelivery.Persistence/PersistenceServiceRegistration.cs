using GloboDelivery.Domain.Interfaces;
using GloboDelivery.Persistence.Data;
using GloboDelivery.Persistence.Models;
using GloboDelivery.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));

            services.AddDbContext<GloboDeliveryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
