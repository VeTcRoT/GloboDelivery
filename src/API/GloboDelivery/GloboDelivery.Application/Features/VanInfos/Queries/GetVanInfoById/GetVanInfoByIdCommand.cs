using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById
{
    public class GetVanInfoByIdCommand : IRequest<GetVanInfoByIdDto>
    {
        public int Id { get; set; }
    }
}
