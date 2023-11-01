using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveriesByCondition
{
    public class GetDeliveriesByConditionQuery : PaginationModel, IRequest<PagedList<FullDeliveryDto>>
    {
        public decimal MinCapacity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureCountry { get; set; } = string.Empty;
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCountry { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
    }
}
