using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.ShippingMethods;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/in-store/{storeKey}/shipping-methods")]
    public class StoreShippingController : ControllerBase
    {
        private readonly IShippingService _ShippingService;

        public StoreShippingController(IShippingService ShippingService)
        {
            _ShippingService = ShippingService;
        }

        [HttpGet("matching-cart")]
        public async Task<ActionResult<IShippingMethodPagedQueryResponse>> Get([FromRoute] string storeKey, [FromQuery] string cartId)
        {
            try
            {
                var ShippingMethods = await _ShippingService.GetShippingMethodsMatchingInStoreCartAsync(storeKey, cartId);
                return Ok(ShippingMethods);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

    }
}
