using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery
{
    public class UpdateDeliveryCommand : IRequest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public int VanInfoId { get; set; }
        public IEnumerable<int> AddressesIds { get; set; } = new List<int>();
    }
}
