using GloboDelivery.Application.Features.Addresses.Commands.CreateAddress;
using GloboDelivery.Application.Features.Addresses.Commands.DeleteAddress;
using GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress;
using GloboDelivery.Application.Features.Addresses.Queries.GetAddressById;
using GloboDelivery.Application.Features.Addresses.Queries.GetAllAddresses;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetAllAddresses()
        {
            var addresses = await _mediator.Send(new GetAllAddressesQuery());

            return Ok(addresses);
        }

        [HttpGet("{Id}", Name = nameof(GetAddressById))]
        public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] GetAddressByIdQuery query)
        {
            var address = await _mediator.Send(query);

            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddress(CreateAddressCommand command)
        {
            var address = await _mediator.Send(command);

            return CreatedAtRoute(nameof(GetAddressById), new { address.Id }, address);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAddress([FromRoute] DeleteAddressCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
