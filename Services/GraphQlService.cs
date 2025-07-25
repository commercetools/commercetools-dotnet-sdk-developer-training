using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.GraphQl;

namespace Training.Services
{
    public interface IGraphQlService
    {
        Task<IGraphQLResponse> PostGraphQlQuery(string storeKey, string email);
    }

    public class GraphQlService : IGraphQlService
    {
        private readonly ProjectApiRoot _api;
        public GraphQlService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IGraphQLResponse> PostGraphQlQuery(string storeKey, string email)
        {
            var storeId = await GetStoreIdByKeyAsync(storeKey);

            var query = @"
                query MyQuery($storeKey: KeyReferenceInput!, $where: String!) {
                    inStore(key: $storeKey) {
                        orders(where: $where) {
                            results {
                                customerEmail
                                totalPrice {
                                    centAmount
                                    currencyCode
                                }
                            }
                        }
                    }
                }";

            var variables = new GraphQLVariablesMap
            {
                ["storeKey"] = storeKey,
                ["where"] = $"customerEmail=\"{email}\""
            };


            var request = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };
            return await _api
                .Graphql()
                .Post(request)
                .ExecuteAsync();
        }
        
        public async Task<string> GetStoreIdByKeyAsync(string storeKey)
        {
            var store = await _api.Stores().WithKey(storeKey).Get().ExecuteAsync();
            return store.Id;
        }
    }
}
