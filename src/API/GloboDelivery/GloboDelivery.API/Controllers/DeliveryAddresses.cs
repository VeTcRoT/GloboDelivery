using GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Controllers
{
    [Route("api/deliveries/{DeliveryId}/addresses")]
    [ApiController]
    public class DeliveryAddresses : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryAddresses(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetDeliveryAddresses([FromRoute] GetDeliveryAddressesQuery query)
        {
            var addresses = await _mediator.Send(query);

            return Ok(addresses);
        }
    }
}
