using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.ShippingMethods;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingMethodsController : ControllerBase
    {
        private readonly IShippingService _ShippingService;

        public ShippingMethodsController(IShippingService ShippingService)
        {
            _ShippingService = ShippingService;
        }

        [HttpGet()]
        public async Task<ActionResult<IShippingMethodPagedQueryResponse>> Get()
        {
            try
            {
                var ShippingMethods = await _ShippingService.GetShippingMethodsAsync();
                return Ok(ShippingMethods);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpHead("{key}")]
        public async Task<ActionResult> CheckExists(string key)
        {
            try
            {
                var exists = await _ShippingService.CheckShippingMethodExistsAsync(key);
                return exists ? Ok() : NotFound();
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IShippingMethod>> Get(string id)
        {
            try
            {
                var ShippingMethod = await _ShippingService.GetShippingMethodByKeyAsync(id);
                return Ok(ShippingMethod);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpGet("matching-location")]
        public async Task<ActionResult<IShippingMethodPagedQueryResponse>> GetByLocation([FromQuery] string country)
        {
            try
            {
                var ShippingMethods = await _ShippingService.GetShippingMethodsByLocationAsync(country);
                return Ok(ShippingMethods);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

    }
    
}
