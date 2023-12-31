﻿using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo
{
    public class CreateVanInfoCommand : IRequest<VanInfoDto>
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
