using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Commands.DeleteVanInfo
{
    public class DeleteVanInfoCommand : IRequest
    {
        public int Id { get; set; }
    }
}
