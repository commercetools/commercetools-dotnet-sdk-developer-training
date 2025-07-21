using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.Customers;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICustomer>> Get(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ICustomer>> Post([FromBody] ICustomerDraft customerDraft)
        {
            var customer = await _customerService.CreateCustomerAsync(customerDraft);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }
    }
}
