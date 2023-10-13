using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdQuery : IRequest
    {
        public int Id { get; set; }
    }
}
