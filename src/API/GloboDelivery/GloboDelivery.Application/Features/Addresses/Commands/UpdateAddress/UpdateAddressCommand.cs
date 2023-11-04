using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : AddressBaseCommand, IRequest
    {
        public int Id { get; set; }
    }
}
