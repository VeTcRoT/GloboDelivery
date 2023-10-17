using GloboDelivery.Application.Models;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQuery : PaginationModel, IRequest<PagedList<VanInfoDto>>
    {
    }
}
