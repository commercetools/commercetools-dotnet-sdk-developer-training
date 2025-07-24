using commercetools.Sdk.Api.Models.CustomObjects;
using commercetools.Sdk.Api.Models.Types;
using Microsoft.AspNetCore.Mvc;
using Training.Services;
using Training.ViewModels;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtensionsController : ControllerBase
    {
        private readonly IExtensionsService _extensionsService;

        public ExtensionsController(IExtensionsService extensionsService)
        {
            _extensionsService = extensionsService;
        }

        [HttpGet("custom-objects/{container}/{key}")]
        public async Task<ActionResult<ICustomObject>> Get([FromRoute] string container, [FromRoute] string key)
        {
            var customObject = await _extensionsService.GetCustomObjectByContainerAndKeyAsync(container, key);
            return Ok(customObject);
        }


        [HttpPost("custom-objects")]
        public async Task<ActionResult<ICustomObject>> CreateCustomObject([FromBody] CustomObjectCreateRequest customObjectCreateRequest)
        {
            var customObject = await _extensionsService.CreateCustomObjectAsync(customObjectCreateRequest);
            return Ok(customObject);
        }

        [HttpPost("types")]
        public async Task<ActionResult<IType>> Create()
        {
            var type = await _extensionsService.CreateTypeAsync();
            return Ok(type);
        }
    }
}