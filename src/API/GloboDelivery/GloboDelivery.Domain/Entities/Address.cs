namespace GloboDelivery.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string AdministrativeArea { get; set; } = string.Empty; 
        public string AddressLine { get; set; } = string.Empty;
        public int SuiteNumber { get; set; }
        public string PostalCode { get; set; } = string.Empty;
    }
}
