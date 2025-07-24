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
                Console.WriteLine($"{cartCreateRequest.Sku}");
                var cart = await _cartsService.CreateCart(storeKey, cartCreateRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{id}/lineitems")]
        public async Task<ActionResult<ICart>> PostUpdate([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateRequest cartUpdateRequest)
        {
            try
            {
                var cart = await _cartsService.UpdateCart(storeKey, id, cartUpdateRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{id}/code")]
        public async Task<ActionResult<ICart>> PostUpdateCode([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateCodeRequest cartUpdateCodeRequest)
        {
            try
            {
                var cart = await _cartsService.UpdateCartCode(storeKey, id, cartUpdateCodeRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{id}/shipping-address")]
        public async Task<ActionResult<ICart>> PostUpdateShippingAddress([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateShippingAddressRequest cartUpdateShippingAddressRequest)
        {
            try
            {
                var cart = await _cartsService.UpdateCartShippingAddress(storeKey, id, cartUpdateShippingAddressRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }
        
        [HttpPost("{id}/shipping-method")]
        public async Task<ActionResult<ICart>> PostUpdateShippingMethod([FromRoute] string storeKey, [FromRoute] string id, [FromBody] CartUpdateShippingMethodRequest cartUpdateShippingMethodRequest)
        {
            try
            {
                var cart = await _cartsService.UpdateCartShippingMethod(storeKey, id, cartUpdateShippingMethodRequest);
                return Ok(cart);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

    }
}
