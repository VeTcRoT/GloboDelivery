using MediatR;

namespace GloboDelivery.Application.Features.Addresses.DeleteAddress
{
    public class DeleteAddressCommand : IRequest
    {
        public int Id { get; set; }
    }
}
