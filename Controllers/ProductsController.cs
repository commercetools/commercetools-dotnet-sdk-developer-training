using Microsoft.AspNetCore.Mvc;
using Training.Services;
using commercetools.Sdk.Api.Models.Products;
using commercetools.Sdk.Api.Models.ProductSearches;
using Training.ViewModels;

namespace Training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _ProductsService;

        public ProductsController(IProductsService ProductsService)
        {
            _ProductsService = ProductsService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IProductPagedSearchResponse>> Search([FromQuery] ProductSearchViewModel productSearchViewModel)
        {
            var products = await _ProductsService.SearchProductsAsync(productSearchViewModel);
            return Ok(products);
        }

        [HttpGet()]
        public async Task<ActionResult<IProductPagedQueryResponse>> Get()
        {
            var products = await _ProductsService.GetProductsAsync();
            return Ok(products);
        }

        [HttpHead("{key}")]
        public async Task<ActionResult> CheckExists(string key)
        {
            try
            {
                var exists = await _ProductsService.CheckProductExistsAsync(key);
                return exists ? Ok() : NotFound();
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<IProduct>> Get(string key)
        {
            try
            {
                var product = await _ProductsService.GetProductByKeyAsync(key);
                return Ok(product);
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                return NotFound();
            }
        }

    }
}
