using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries
{
    public class GetAllDeliveriesQuery : PaginationModel, IRequest<PagedList<DeliveryDto>>
    {
    }
}
