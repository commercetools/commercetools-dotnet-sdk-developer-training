using commercetools.Sdk.ImportApi.Models.Importrequests;
using commercetools.Sdk.ImportApi.Models.Importsummaries;
using Microsoft.AspNetCore.Mvc;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportsController : ControllerBase
    {
        private readonly IImportsService _importsService;

        public ImportsController(IImportsService importsService)
        {
            _importsService = importsService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<IImportResponse>> ImportProducts([FromForm] IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var importResponse = await _importsService.ImportProductDraftsAsync(stream);
            return Ok(importResponse);
        }


        [HttpGet("summary")]
        public async Task<ActionResult<IImportSummary>> GetSummary()
        {
            var summary = await _importsService.GetSummaryByContainerAsync();
            return Ok(summary);
        }
    }
}