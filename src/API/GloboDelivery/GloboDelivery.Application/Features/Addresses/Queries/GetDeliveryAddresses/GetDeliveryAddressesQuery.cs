using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses
{
    public class GetDeliveryAddressesQuery : PaginationModel, IRequest<PagedList<DeliveryAddressListingDto>>
    {
        public int DeliveryId { get; set; }
    }
}
