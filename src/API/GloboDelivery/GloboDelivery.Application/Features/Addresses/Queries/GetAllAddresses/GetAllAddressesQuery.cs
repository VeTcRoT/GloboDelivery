using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQuery : PaginationModel, IRequest<IReadOnlyList<AddressDto>>
    {
    }
}
