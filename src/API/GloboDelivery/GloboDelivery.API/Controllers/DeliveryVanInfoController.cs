using GloboDelivery.Application.Features.VanInfos.Queries.GetDeliveryVanInfo;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Controllers
{
    [Route("api/deliveries/{DeliveryId}/vaninfos")]
    [ApiController]
    public class DeliveryVanInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryVanInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<VanInfoDto>> GetDeliveryVanInfo([FromRoute] GetDeliveryVanInfoQuery query)
        {
            var vanInfo = await _mediator.Send(query);

            return Ok(vanInfo);
        }
    }
}
