using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses
{
    public class GetDeliveryAddressesQuery : IRequest<IReadOnlyList<AddressDto>>
    {
        public int DeliveryId { get; set; }
    }
}
