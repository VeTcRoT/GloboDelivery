namespace GloboDelivery.Domain.Dtos
{
    public class DeliveryDto
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
