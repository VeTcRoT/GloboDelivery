using AutoMapper;
using GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery;
using GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Application.MappingProfiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<CreateDeliveryCommand, Delivery>();
            CreateMap<Delivery, CreateDeliveryDto>();
            CreateMap<UpdateDeliveryCommand, Delivery>();
            CreateMap<Delivery, DeliveryDto>();

            CreateMap<PagedList<Delivery>, PagedList<DeliveryDto>>()
                .ConvertUsing<PagedListConverter<Delivery, DeliveryDto>>();
        }
    }
}
