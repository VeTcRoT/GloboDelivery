using GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo;
using GloboDelivery.Application.Features.VanInfos.Commands.DeleteVanInfo;
using GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo;
using GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos;
using GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Controllers
{
    [Route("api/vaninfos")]
    [ApiController]
    public class VanInfosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VanInfosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VanInfoDto>>> GetVanInfos()
        {
            var vanInfos = await _mediator.Send(new GetAllVanInfosQuery());

            return Ok(vanInfos);
        }

        [HttpGet("{Id}", Name = nameof(GetVanInfoById))]
        public async Task<ActionResult<VanInfoDto>> GetVanInfoById([FromRoute] GetVanInfoByIdQuery query)
        {
            var vanInfo = await _mediator.Send(query);

            return Ok(vanInfo);
        }

        [HttpPost]
        public async Task<ActionResult<VanInfoDto>> CreateVanInfo(CreateVanInfoCommand command)
        {
            var vanInfo = await _mediator.Send(command);

            return CreatedAtRoute(nameof(GetVanInfoById), new { vanInfo.Id }, vanInfo);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVanInfo(UpdateVanInfoCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVanInfo([FromRoute] DeleteVanInfoCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
