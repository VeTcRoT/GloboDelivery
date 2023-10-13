using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.CreateAddress
{
    public class CreateAddressCommand : IRequest<AddressDto>
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string AdministrativeArea { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public int SuiteNumber { get; set; }
        public string PostalCode { get; set; } = string.Empty;
    }
}
