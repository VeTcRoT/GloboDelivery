namespace GloboDelivery.Domain.Dtos
{
    public class DeliveryListingDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
    }
}
