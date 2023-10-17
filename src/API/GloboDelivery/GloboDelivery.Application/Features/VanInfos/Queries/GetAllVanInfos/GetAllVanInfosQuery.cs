using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQuery : PaginationModel, IRequest<IReadOnlyList<VanInfoDto>>
    {
    }
}
