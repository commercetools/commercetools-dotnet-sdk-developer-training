using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.Carts;
using Training.Services;
using Training.ViewModels;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/in-store/{storeKey}/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartsService;

        public CartsController(ICartsService cartsService)
        {
            _cartsService = cartsService;
        }

        [HttpGet()]
        public async Task<ActionResult<ICartPagedQueryResponse>> Get([FromRoute] string storeKey)
        {
            try
            {
                var carts = await _cartsService.GetCartsAsync(storeKey);
                return Ok(carts);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ICart>> Get([FromRoute] string storeKey, string id)
        {
            try
            {
                var cart = await _cartsService.GetCartByIdAsync(storeKey, id);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ICart>> Post([FromRoute] string storeKey, [FromBody] CartCreateRequest cartCreateRequest)
        {
            try
            {
                var cart = await _cartsService.CreateCartAsync(storeKey, cartCreateRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{id}/lineitems")]
        public async Task<ActionResult<ICart>> AddLineItem([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateRequest cartUpdateRequest)
        {
            try
            {
                var cart = await _cartsService.AddLineItemAsync(storeKey, id, cartUpdateRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 409)
            {
                return Conflict("Conflict occurred while updating the cart.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{id}/discount-code")]
        public async Task<ActionResult<ICart>> AddDiscountCode([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateCodeRequest cartUpdateCodeRequest)
        {
            try
            {
                var cart = await _cartsService.AddDiscountCodeAsync(storeKey, id, cartUpdateCodeRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{id}/shipping-address")]
        public async Task<ActionResult<ICart>> SetShippingAddress([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateShippingAddressRequest cartUpdateShippingAddressRequest)
        {
            try
            {
                var cart = await _cartsService.SetShippingAddressAsync(storeKey, id, cartUpdateShippingAddressRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }
        
        [HttpPost("{id}/shipping-method")]
        public async Task<ActionResult<ICart>> SetShippingMethod([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateShippingMethodRequest cartUpdateShippingMethodRequest)
        {
            try
            {
                var cart = await _cartsService.SetShippingMethodAsync(storeKey, id, cartUpdateShippingMethodRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }
    }
}
