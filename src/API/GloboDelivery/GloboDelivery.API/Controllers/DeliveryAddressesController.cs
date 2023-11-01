using GloboDelivery.API.Helpers;
using GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses;
using GloboDelivery.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GloboDelivery.API.Controllers
{
    [Route("api/deliveries/{DeliveryId}/deliveryaddresses")]
    [ApiController]
    public class DeliveryAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetDeliveryAddresses))]
        public async Task<ActionResult<IReadOnlyList<DeliveryAddressListingDto>>> GetDeliveryAddresses([FromRoute] int DeliveryId, int PageNumber, int PageSize)
        {
            var addresses = await _mediator.Send(new GetDeliveryAddressesQuery() { DeliveryId = DeliveryId, PageNumber = PageNumber, PageSize = PageSize });

            Response.Headers.Add("X-Pagination", 
                JsonSerializer.Serialize(
                    PaginationMetadataHelper.CreatePaginationMetadata<DeliveryAddressListingDto, GetDeliveryAddressesQuery>(
                        addresses, Url, nameof(GetDeliveryAddresses))));

            return Ok(addresses);
        }
    }
}
