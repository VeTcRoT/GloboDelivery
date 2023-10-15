using GloboDelivery.Domain.Dtos;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetDeliveryVanInfo
{
    public class GetDeliveryVanInfoQuery : IRequest<VanInfoDto>
    {
        public int DeliveryId { get; set; }
    }
}
