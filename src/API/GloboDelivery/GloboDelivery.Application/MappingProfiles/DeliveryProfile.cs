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

            CreateMap<Delivery, FullDeliveryDto>()
                .ForMember(
                    dest => dest.VanInfo,
                    opt => opt.MapFrom(src => src.VanInfo)
                )
                .ForMember(
                    dest => dest.Addresses,
                    opt => opt.MapFrom(src => src.DeliveryAddresses.Select(da =>
                        new DeliveryAddressListingDto
                        {
                            Id = da.Address.Id,
                            Country = da.Address.Country,
                            City = da.Address.City,
                            AdministrativeArea = da.Address.AdministrativeArea,
                            AddressLine = da.Address.AddressLine,
                            SuiteNumber = da.Address.SuiteNumber,
                            PostalCode = da.Address.PostalCode,
                            DepartureDate = da.DepartureDate,
                            ArrivalDate = da.ArrivalDate
                        }
                    ))
                );

            CreateMap<PagedList<Delivery>, PagedList<FullDeliveryDto>>()
                .ConvertUsing<PagedListConverter<Delivery, FullDeliveryDto>>();
        }
    }
}
