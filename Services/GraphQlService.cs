using System.Text.Json;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.CustomObjects;
using commercetools.Sdk.Api.Models.GraphQl;
using commercetools.Sdk.Api.Models.Types;
using Training.ViewModels;

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

            // TODO: use Graphql endpoint to run the above query
            throw new NotImplementedException("This method is not yet implemented.");

        }
        
        public async Task<string> GetStoreIdByKeyAsync(string storeKey)
{
            var store = await _api.Stores().WithKey(storeKey).Get().ExecuteAsync();
            return store.Id;
        }
    }
}
