using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.GraphQl;
using Training.ViewModels;

namespace Training.Services
{
    public interface IGraphQlService
    {
        Task<IGraphQLResponse> PostGraphQlQuery(GraphQlRequest graphQlRequest);
    }

    public class GraphQlService : IGraphQlService
    {
        private readonly ProjectApiRoot _api;
        public GraphQlService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IGraphQLResponse> PostGraphQlQuery(GraphQlRequest graphQlRequest)
        {
            var query = @"
                query MyQuery($storeKey: KeyReferenceInput!, $locale: Locale!, $where: String!) {
                    inStore(key: $storeKey) {
                        orders(where: $where) {
                            results {
                                customerEmail
                                customer {
                                    firstName
                                    lastName
                                }
                                lineItems {
                                    name(locale: $locale)
                                    totalPrice {centAmount currencyCode}
                                }
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
                ["storeKey"] = graphQlRequest.StoreKey,
                ["where"] = $"customerEmail=\"{graphQlRequest.Email}\"",
                ["locale"] = graphQlRequest.Locale
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
    }
}
