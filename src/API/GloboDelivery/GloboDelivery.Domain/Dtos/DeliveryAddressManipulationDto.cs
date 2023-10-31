namespace GloboDelivery.Domain.Dtos
{
    public class DeliveryAddressManipulationDto
    {
        public int AddressId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
