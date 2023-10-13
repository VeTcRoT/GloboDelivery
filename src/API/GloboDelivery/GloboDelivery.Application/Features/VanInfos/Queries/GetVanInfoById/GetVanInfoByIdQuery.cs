using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById
{
    public class GetVanInfoByIdCommand : IRequest<VanInfoDto>
    {
        public int Id { get; set; }
    }
}
