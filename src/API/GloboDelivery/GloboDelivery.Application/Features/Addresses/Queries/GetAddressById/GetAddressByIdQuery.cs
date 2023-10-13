using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdQuery : IRequest<AddressDto>
    {
        public int Id { get; set; }
    }
}
