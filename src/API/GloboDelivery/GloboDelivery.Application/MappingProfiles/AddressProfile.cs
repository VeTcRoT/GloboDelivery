using AutoMapper;
using GloboDelivery.Application.Features.Addresses.CreateAddress;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Dtos;

namespace GloboDelivery.Application.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}
