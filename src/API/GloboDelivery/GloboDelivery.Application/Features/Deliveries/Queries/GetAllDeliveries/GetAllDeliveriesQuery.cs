using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries
{
    public class GetAllDeliveriesQuery : IRequest<IReadOnlyList<DeliveryListingDto>>
    {
    }
}
