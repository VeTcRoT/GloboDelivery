using GloboDelivery.Domain.Dtos;
using SmartyStreets.USStreetApi;

namespace GloboDelivery.Application.Services.Infrastructure
{
    public interface IAddressValidationService
    {
        Candidate? ValidateAddress(AddressBaseCommand address);
    }
}
