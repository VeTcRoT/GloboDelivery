namespace GloboDelivery.Domain.Entities
{
    public class DeliveryAddress : BaseEntity
    {
        public int DeviveryId { get; set; }
        public int AddressId { get; set; }

        public Delivery Delivery { get; set; } = null!;
        public Address Address { get; set; } = null!;
    }
}
