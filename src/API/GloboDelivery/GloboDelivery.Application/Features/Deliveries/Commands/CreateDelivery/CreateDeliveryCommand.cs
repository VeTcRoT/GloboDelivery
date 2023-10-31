using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery
{
    public class CreateDeliveryCommand : IRequest<CreateDeliveryDto>
    {
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public int VanInfoId { get; set; }
        public IEnumerable<AddressDate> AddressesDates { get; set; } = new List<AddressDate>();
    }

    public record AddressDate(int AddressId, DateTime DepartureDate, DateTime ArrivalDate);
}
