using SmartyStreets.USStreetApi;

namespace GloboDelivery.Infrastructure.Interfaces.Services
{
    public interface IAddressValidationClient
    {
        Client GetClient();
    }
}