namespace GloboDelivery.Domain.Dtos
{
    public class FullDeliveryDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public VanInfoDto VanInfo { get; set; } = null!;
        public IReadOnlyList<DeliveryAddressListingDto> Addresses { get; set; } = null!;
    }
}
