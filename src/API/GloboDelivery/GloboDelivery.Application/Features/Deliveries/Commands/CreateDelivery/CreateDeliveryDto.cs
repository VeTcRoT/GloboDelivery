using GloboDelivery.Domain.Dtos;

namespace GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery
{
    public class CreateDeliveryDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public VanInfoDto VanInfo { get; set; } = null!;
        public IReadOnlyList<AddressDto> Addresses { get; set; } = null!;
    }
}
