using AutoMapper;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Application.Features.Addresses.Commands.CreateAddress;
using GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Application.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<UpdateAddressCommand, Address>();
            CreateMap<Address, DeliveryAddressListingDto>();
            CreateMap<DeliveryAddress, DeliveryAddressListingDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Address.Id)
                )
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Address.Country)
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.Address.City)
                )
                .ForMember(
                    dest => dest.AdministrativeArea,
                    opt => opt.MapFrom(src => src.Address.AdministrativeArea)
                )
                .ForMember(
                    dest => dest.AddressLine,
                    opt => opt.MapFrom(src => src.Address.AddressLine)
                )
                .ForMember(
                    dest => dest.SuiteNumber,
                    opt => opt.MapFrom(src => src.Address.SuiteNumber)
                )
                .ForMember(
                    dest => dest.PostalCode,
                    opt => opt.MapFrom(src => src.Address.PostalCode)
                )
                .ForMember(
                    dest => dest.DepartureDate,
                    opt => opt.MapFrom(src => src.DepartureDate)
                )
                .ForMember(
                    dest => dest.ArrivalDate,
                    opt => opt.MapFrom(src => src.ArrivalDate)
                );

            CreateMap<PagedList<DeliveryAddress>, PagedList<DeliveryAddressListingDto>>()
                .ConvertUsing<PagedListConverter<DeliveryAddress, DeliveryAddressListingDto>>();

            CreateMap<PagedList<Address>, PagedList<AddressDto>>()
                .ConvertUsing<PagedListConverter<Address, AddressDto>>();
        }
    }
}
