namespace GloboDelivery.Domain.Entities
{
    public class DeliveryAddress : BaseEntity
    {
        public int DeliveryId { get; set; }
        public int AddressId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public Delivery Delivery { get; set; } = null!;
        public Address Address { get; set; } = null!;
    }
}
