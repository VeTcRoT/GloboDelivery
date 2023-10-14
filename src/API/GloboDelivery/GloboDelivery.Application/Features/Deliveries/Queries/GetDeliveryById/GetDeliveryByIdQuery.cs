using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveryById
{
    public class GetDeliveryByIdQuery : IRequest<DeliveryListingDto>
    {
        public int Id { get; set; }
    }
}
