using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Services.Infrastructure;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Infrastructure.Interfaces.Services;
using SmartyStreets.USStreetApi;

namespace GloboDelivery.Infrastructure.Services
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly IAddressValidationClient _addressValidationClient;

        public AddressValidationService(IAddressValidationClient addressValidationClient)
        {
            _addressValidationClient = addressValidationClient;
        }

        public Candidate? ValidateAddress(AddressBaseCommand address)
        {
            var client = _addressValidationClient.GetClient();

            var lookup = new Lookup
            {
                Street = address.AddressLine,
                Secondary = address.SuiteNumber.ToString(),
                City = address.City,
                State = address.AdministrativeArea,
                ZipCode = address.PostalCode,
                MatchStrategy = Lookup.STRICT
            };

            client.Send(lookup);

            var candidates = lookup.Result;

            if (candidates.Count == 0)
                throw new ValidationException("Address is invalid. Try enter another address.");

            return candidates[0];
        }
    }
}
