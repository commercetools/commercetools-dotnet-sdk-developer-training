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
                        Filter = CreateSortFilter()
                    }
                }           
            };

            if (searchRequest.IncludeFacets)
            {
                productSearchRequest.Facets = CreateFacets(searchRequest.Locale);
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


        private ISearchQuery CreateSortFilter()
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
                            Value = "EUR"
                        }
                    },
                    new SearchExactExpression()
                    {
                        Exact = new SearchExactValue()
                        {
                            Field = "variants.prices.country",
                            FieldType = ISearchFieldType.Text,
                            Value = "DE"
                        }
                    }
                }
            };
        }
        private List<IProductSearchFacetExpression> CreateFacets(string locale)
        {
            return new List<IProductSearchFacetExpression>
                {
                    new ProductSearchFacetDistinctExpression{
                        Distinct = new ProductSearchFacetDistinctValue{
                            Name = "Color",
                            Field = "variants.attributes.search-color.label",
                            FieldType = ISearchFieldType.Lenum,
                            Language = locale,
                            Level = IProductSearchFacetCountLevelEnum.Variants,
                            Scope = IProductSearchFacetScopeEnum.All
                        }
                    },
                    new ProductSearchFacetDistinctExpression{
                        Distinct = new ProductSearchFacetDistinctValue{
                            Name = "Finish",
                            Field = "variants.attributes.search-finish.label",
                            FieldType = ISearchFieldType.Lenum,
                            Language = locale,
                            Level = IProductSearchFacetCountLevelEnum.Variants,
                            Scope = IProductSearchFacetScopeEnum.All
                        }
                    }
                };
        }

        private async Task<ISearchQuery> CreateSearchQuery(SearchRequest searchRequest)
        {
            return new SearchAndExpression{
                And = new List<ISearchQuery>{
                    new SearchFullTextExpression
                    {
                        FullText = new SearchFullTextValue
                        {
                            Field = "name",
                            Value = searchRequest.Keyword,
                            Language = searchRequest.Locale,
                            MustMatch = ISearchMatchType.Any
                        }
                    },
                    new SearchExactExpression
                    {
                        Exact = new SearchExactValue
                        {
                            Field = "stores",
                            Value = await GetStoreIdByKeyAsync(searchRequest.StoreKey),
                            FieldType = ISearchFieldType.SetReference
                        }
                    }
                }
            };
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

        public async Task<string> GetStoreIdByKeyAsync(string storeKey)
        {
            var store = await _api.Stores().WithKey(storeKey).Get().ExecuteAsync();
            return store.Id;
        }
    }
}
