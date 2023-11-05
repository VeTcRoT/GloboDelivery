using GloboDelivery.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace GloboDelivery.Infrastructure.Services
{
    public class AddressValidationClient : IAddressValidationClient
    {
        private readonly IConfiguration _configuration;

        public AddressValidationClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Client GetClient()
        {
            var authId = _configuration["SmartyStreets:AuthId"];
            var authToken = _configuration["SmartyStreets:AuthToken"];

            var client = new ClientBuilder(authId, authToken).WithLicense(new List<string> { "us-core-cloud" })
                .BuildUsStreetApiClient();

            return client;
        }
    }
}
