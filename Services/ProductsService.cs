using commercetools.Sdk.Api.Client;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Models.Products;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.ProductSearches;
using commercetools.Sdk.Api.Models.Searches;


namespace Training.Services
{
    public interface IProductsService
    {
        Task<IProduct> GetProductByKeyAsync(string key);
        Task<IProductPagedQueryResponse> GetProductsAsync();
        Task<bool> CheckProductExistsAsync(string key);
        Task<IProductPagedSearchResponse> SearchProductsAsync(SearchRequest searchRequest);

    }

    public class ProductsService : IProductsService
    {
        private readonly ProjectApiRoot _api;

        public ProductsService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IProductPagedSearchResponse> SearchProductsAsync(SearchRequest searchRequest)
        {
    
            ProductSearchRequest productSearchRequest = new ProductSearchRequest
            {
                ProductProjectionParameters = new ProductSearchProjectionParams
                {
                    PriceCountry = searchRequest.Country,
                    PriceCurrency = searchRequest.Currency,
                    StoreProjection = searchRequest.StoreKey
                },
                Sort = new List<ISearchSorting>
                {
                    new SearchSorting
                    {
                        Field = "variants.prices.centAmount",
                        Mode = ISearchSortMode.Min,
                        Order = ISearchSortOrder.Asc,
                        Filter = CreateSortFilter(searchRequest)
                    }
                }           
            };

            if (searchRequest.IncludeFacets)
            {
                productSearchRequest.Facets = CreateFacets();
            }
            if (searchRequest.Keyword != null)
            {
                productSearchRequest.Query = await CreateSearchQuery(searchRequest);
            }

            return await _api.Products()
                .Search()
                .Post(productSearchRequest)
                .ExecuteAsync();
        }

        private ISearchQuery CreateSortFilter(SearchRequest searchRequest)
        {
            return new SearchAndExpression()
            {
                And = new List<ISearchQuery>()
                {
                    new SearchExactExpression()
                    {
                        Exact = new SearchExactValue()
                        {
                            Field = "variants.prices.currencyCode",
                            FieldType = ISearchFieldType.Text,
                            Value = searchRequest.Currency
                        }
                    },
                    new SearchExactExpression()
                    {
                        Exact = new SearchExactValue()
                        {
                            Field = "variants.prices.country",
                            FieldType = ISearchFieldType.Text,
                            Value = searchRequest.Country
                        }
                    }
                }
            };
        }
        private List<IProductSearchFacetExpression> CreateFacets()
        {
            return new List<IProductSearchFacetExpression>();
            // TODO: Add Facet expressions
        }

        private Task<ISearchQuery> CreateSearchQuery(SearchRequest searchRequest)
        {
            // TODO: Add Search Query expressions to filter by keyword and store
            throw new NotImplementedException("This functions is not implemented yet");

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
                // If we get a 404, the product doesn't exist
                return false;
            }


        }


    }
}
