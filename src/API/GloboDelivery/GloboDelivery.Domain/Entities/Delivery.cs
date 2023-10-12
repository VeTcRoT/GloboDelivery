namespace GloboDelivery.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
        public int VanInfoId { get; set; }

        public VanInfo VanInfo { get; set; } = null!;
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
            = new List<DeliveryAddress>();
    }
}
