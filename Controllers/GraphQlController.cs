using Microsoft.AspNetCore.Mvc;
using Training.Services;
using commercetools.Sdk.Api.Models.GraphQl;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/in-store/{storeKey}/[controller]")]
    public class GraphQlController : ControllerBase
    {
        private readonly IGraphQlService _graphQlService;

        public GraphQlController(IGraphQlService graphQlService)
        {
            _graphQlService = graphQlService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IGraphQLResponse>> Get([FromRoute] string storeKey, [FromQuery] string email)
        {
            try
            {
                var response = await _graphQlService.PostGraphQlQuery(storeKey, email);
                return Ok(response);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }
    }
}
