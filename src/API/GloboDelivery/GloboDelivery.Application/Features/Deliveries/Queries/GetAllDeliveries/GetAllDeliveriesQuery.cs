using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries
{
    public class GetAllDeliveriesQuery : PaginationModel, IRequest<IReadOnlyList<DeliveryDto>>
    {
    }
}
