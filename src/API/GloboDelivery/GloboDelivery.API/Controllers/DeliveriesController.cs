using GloboDelivery.API.Helpers;
using GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery;
using GloboDelivery.Application.Features.Deliveries.Commands.DeleteDelivery;
using GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery;
using GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries;
using GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveriesByCondition;
using GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveryById;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        [HttpGet(Name = nameof(GetAllDeliveries))]
        public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetAllDeliveries([FromQuery] GetAllDeliveriesQuery query)
        {
            var deliveries = await _mediator.Send(query);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(PaginationMetadataHelper.CreatePaginationMetadata(deliveries, Url, nameof(GetAllDeliveries), query)));

            return Ok(deliveries);
        }

        [HttpGet("{Id}", Name = nameof(GetDeliveryById))]
        public async Task<ActionResult<DeliveryDto>> GetDeliveryById([FromRoute] GetDeliveryByIdQuery query)
        {
            var delivery = await _mediator.Send(query);

            return Ok(delivery);
        }

        [HttpGet("getdeliveriesbycondition", Name = nameof(GetDeliveriesByCondition))]
        public async Task<ActionResult<IReadOnlyCollection<FullDeliveryDto>>> GetDeliveriesByCondition([FromQuery] GetDeliveriesByConditionQuery query)
        {
            var deliveries = await _mediator.Send(query);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(PaginationMetadataHelper.CreatePaginationMetadata(deliveries, Url, nameof(GetDeliveriesByCondition), query)));

            return Ok(deliveries);
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
