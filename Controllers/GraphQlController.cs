using Microsoft.AspNetCore.Mvc;
using Training.Services;
using commercetools.Sdk.Api.Models.GraphQl;
using Training.ViewModels;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphQlController : ControllerBase
    {
        private readonly IGraphQlService _graphQlService;

        public GraphQlController(IGraphQlService graphQlService)
        {
            _graphQlService = graphQlService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IGraphQLResponse>> Get([FromQuery] GraphQlRequest graphQlRequest)
        {
            try
            {
                var response = await _graphQlService.PostGraphQlQuery(graphQlRequest);
                return Ok(response);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }
    }
}
