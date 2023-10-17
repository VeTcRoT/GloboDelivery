using AutoMapper;
using GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo;
using GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Application.MappingProfiles
{
    public class VanInfoProfile : Profile
    {
        public VanInfoProfile()
        {
            CreateMap<CreateVanInfoCommand, VanInfo>();
            CreateMap<VanInfo, VanInfoDto>();
            CreateMap<UpdateVanInfoCommand, VanInfo>();

            CreateMap<PagedList<VanInfo>, PagedList<VanInfoDto>>()
                .ConvertUsing<PagedListConverter<VanInfo, VanInfoDto>>();
        }
    }
}
