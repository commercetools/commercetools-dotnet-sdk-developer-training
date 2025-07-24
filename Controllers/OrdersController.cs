using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.Carts;
using Training.Services;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.Orders;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/in-store/{storeKey}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet()]
        public async Task<ActionResult<IOrderPagedQueryResponse>> Get([FromRoute] string storeKey)
        {
            try
            {
                var orders = await _ordersService.GetOrdersAsync(storeKey);
                return Ok(orders);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }


        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<IOrder>> Get([FromRoute] string storeKey, [FromRoute] string orderNumber)
        {
            try
            {
                var order = await _ordersService.GetOrderByOrderNumberAsync(storeKey, orderNumber);
                return Ok(order);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IOrder>> Post([FromRoute] string storeKey, [FromBody] OrderCreateRequest orderCreateRequest)
        {
            try
            {
                var order = await _ordersService.CreateOrder(storeKey, orderCreateRequest);
                return Ok(order);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("{orderNumber}/custom-fields")]
        public async Task<ActionResult<ICart>> PostUpdate([FromRoute] string storeKey, [FromRoute] string orderNumber, [FromBody] OrderCustomFieldsRequest orderCustomFieldsRequest)
        {
            try
            {
                var order = await _ordersService.UpdateOrderCustomFields(storeKey, orderNumber, orderCustomFieldsRequest);
                return Ok(order);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 401)
            {
                return BadRequest("Bad request");
            }
        }

    }
}
