using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQuery : IRequest<IReadOnlyList<VanInfoDto>>
    {
    }
}
