using AutoMapper;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Application.Features.Addresses.UpdateAddress;
using GloboDelivery.Application.Features.Addresses.Commands.CreateAddress;

namespace GloboDelivery.Application.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<UpdateAddressCommand, Address>();
        }
    }
}
