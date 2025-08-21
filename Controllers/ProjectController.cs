using Microsoft.AspNetCore.Mvc;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;

        public ProjectController(IProjectService ProjectService)
        {
            _ProjectService = ProjectService;
        }

        [HttpGet("countries")]
        public async Task<ActionResult<List<string>>> GetCountries()
        {
            var countries = await _ProjectService.GetCountriesAsync();
            return Ok(countries);
        }
        
        [HttpGet("stores")]
        public async Task<ActionResult<List<string>>> GetStores()
        {
            var stores = await _ProjectService.GetStoresAsync();
            return Ok(stores);
        }

    }
}
