using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQuery : PaginationModel, IRequest<PagedList<AddressDto>>
    {
    }
}
