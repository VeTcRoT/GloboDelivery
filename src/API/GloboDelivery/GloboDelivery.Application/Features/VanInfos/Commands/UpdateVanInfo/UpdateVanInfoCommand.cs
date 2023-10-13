using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo
{
    public class UpdateVanInfoCommand : IRequest
    {
        public string Mark { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Capacity { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public string? Remarks { get; set; }
    }
}
