using AutoMapper;
using GloboDelivery.Application.Features.Deliveries.Commands;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;

namespace GloboDelivery.Application.MappingProfiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<CreateDeliveryCommand, Delivery>();
            CreateMap<Delivery, DeliveryDto>();
        }
    }
}
