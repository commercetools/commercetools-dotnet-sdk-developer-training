using commercetools.Sdk.Api.Client;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Models.Products;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.ProductSearches;


namespace Training.Services
{
    public interface IProductsService
    {
        Task<IProduct> GetProductByKeyAsync(string key);
        Task<IProductPagedQueryResponse> GetProductsAsync();
        Task<bool> CheckProductExistsAsync(string key);
        Task<IProductPagedSearchResponse> SearchProductsAsync(ProductSearchViewModel productSearchViewModel);

    }

    public class ProductsService : IProductsService
    {
        private readonly ProjectApiRoot _api;

        public ProductsService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IProductPagedSearchResponse> SearchProductsAsync(ProductSearchViewModel productSearchViewModel)
        {
    
            ProductSearchRequest productSearchRequest = new ProductSearchRequest
            {
                ProductProjectionParameters = new ProductSearchProjectionParams
                {
                    PriceCountry = productSearchViewModel.Country,
                    PriceCurrency = productSearchViewModel.Currency,
                    StoreProjection = productSearchViewModel.StoreKey
                }
            };

            var response = await _api.Products()
                .Search()
                .Post(productSearchRequest)
                .ExecuteAsync();

            return response;
        }


        public async Task<IProductPagedQueryResponse> GetProductsAsync()
        {
            var response = await _api
                .Products()
                .Get()
                .ExecuteAsync();

            return response;
        }

        public async Task<IProduct> GetProductByKeyAsync(string key)
        {
            var response = await _api
                .Products()
                .WithKey(key)
                .Get()
                .ExecuteAsync();

            return response;
        }


        public async Task<bool> CheckProductExistsAsync(string key)
        {
            try
            {
                await _api
                    .Products()
                    .WithKey(key)
                    .Head() // Using Head to check existence without fetching the full resource
                    .ExecuteAsync();

                return true;
            }
            catch (ApiHttpException ex) when (ex.StatusCode == 404)
            {
                // If we get a 404, the shipping method doesn't exist
                return false;
            }


        }


    }
}
