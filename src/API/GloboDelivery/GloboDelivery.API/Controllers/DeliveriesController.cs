using GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery;
using GloboDelivery.Application.Features.Deliveries.Commands.DeleteDelivery;
using GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery;
using GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries;
using GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveryById;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Controllers
{
    [Route("api/deliveries")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetAllDeliveries()
        {
            var deliveries = await _mediator.Send(new GetAllDeliveriesQuery());

            return Ok(deliveries);
        }

        [HttpGet("{Id}", Name = nameof(GetDeliveryById))]
        public async Task<ActionResult<DeliveryDto>> GetDeliveryById([FromRoute] GetDeliveryByIdQuery query)
        {
            var delivery = await _mediator.Send(query);

            return Ok(delivery);
        }

        [HttpPost]
        public async Task<ActionResult<CreateDeliveryDto>> CreateDelivery(CreateDeliveryCommand command)
        {
            var delivery = await _mediator.Send(command);

            return CreatedAtRoute(nameof(GetDeliveryById), new { delivery.Id }, delivery);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDelivery(UpdateDeliveryCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDelivery([FromRoute] DeleteDeliveryCommand command)
        {
            await _mediator.Send(command);  

            return NoContent();
        }
    }
}
