namespace GloboDelivery.Domain.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public decimal CapacityTaken { get; set; }
    }
}
