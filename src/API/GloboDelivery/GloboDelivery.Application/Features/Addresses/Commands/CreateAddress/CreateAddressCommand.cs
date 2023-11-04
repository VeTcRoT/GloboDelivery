using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommand : AddressBaseCommand, IRequest<AddressDto>
    {
    }
}
