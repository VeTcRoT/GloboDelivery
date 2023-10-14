using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Commands.DeleteDelivery
{
    public class DeleteDeliveryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
