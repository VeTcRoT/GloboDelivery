using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveryById
{
    public class GetDeliveryByIdQuery : IRequest<DeliveryDto>
    {
        public int Id { get; set; }
    }
}
