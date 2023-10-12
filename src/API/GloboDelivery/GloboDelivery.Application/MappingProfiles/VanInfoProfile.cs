using AutoMapper;
using GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo;
using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Application.MappingProfiles
{
    public class VanInfoProfile : Profile
    {
        public VanInfoProfile()
        {
            CreateMap<CreateVanInfoCommand, VanInfo>();
            CreateMap<VanInfo, CreateVanInfoDto>();
        }
    }
}
