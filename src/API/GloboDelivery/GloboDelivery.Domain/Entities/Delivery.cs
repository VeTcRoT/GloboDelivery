namespace GloboDelivery.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAvailable { get; set; }
        public int VanInfoId { get; set; }
    }
}
