using Microsoft.AspNetCore.Mvc;
using commercetools.Sdk.Api.Models.Customers;
using Training.Services;
using Training.ViewModels;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/in-store/{storeKey}/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICustomer>> Get([FromRoute] string storeKey, string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(storeKey, id);
                return Ok(customer);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ICustomerSignInResult>> Post([FromRoute] string storeKey, [FromBody] CustomerSignupRequest customerSignupRequest)
        {
            var customerSignInResult = await _customerService.CreateCustomerAsync(storeKey, customerSignupRequest);
            return Ok(customerSignInResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ICustomerSignInResult>> PostSignin([FromRoute] string storeKey, [FromBody] CustomerSigninRequest customerSigninRequest)
        {
            var customerSignInResult = await _customerService.SigninCustomerAsync(storeKey, customerSigninRequest);
            return Ok(customerSignInResult);
        }
    }
}
