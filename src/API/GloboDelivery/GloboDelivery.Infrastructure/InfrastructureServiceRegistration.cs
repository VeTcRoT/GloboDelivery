using GloboDelivery.Application.Services.Infrastructure;
using GloboDelivery.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IAddressValidationClient, AddressValidationClient>();
            services.AddTransient<IAddressValidationService, AddressValidationService>();

            return services;
        }
    }
}
