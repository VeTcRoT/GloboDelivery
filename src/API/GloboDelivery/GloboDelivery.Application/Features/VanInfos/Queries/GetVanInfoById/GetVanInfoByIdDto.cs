namespace GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById
{
    public class GetVanInfoByIdDto
    {
        public int Id { get; set; }
        public string Mark { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Capacity { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public string? Remarks { get; set; }
    }
}
