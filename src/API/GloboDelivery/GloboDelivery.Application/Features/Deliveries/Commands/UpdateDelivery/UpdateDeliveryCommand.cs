using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery
{
    public class UpdateDeliveryCommand : IRequest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public int VanInfoId { get; set; }
        public IEnumerable<DeliveryAddressManipulationDto> AddressesDates { get; set; } = new List<DeliveryAddressManipulationDto>();
    }
}
