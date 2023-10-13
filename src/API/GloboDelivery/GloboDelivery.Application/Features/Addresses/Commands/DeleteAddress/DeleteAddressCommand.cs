using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest
    {
        public int Id { get; set; }
    }
}
