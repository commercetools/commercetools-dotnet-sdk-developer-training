using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.ShippingMethods;
using commercetools.Base.Client;


namespace Training.Services
{
    public interface IShippingService
    {
        Task<IShippingMethod> GetShippingMethodByKeyAsync(string key);
        Task<IShippingMethodPagedQueryResponse> GetShippingMethodsAsync();

        Task<IShippingMethodPagedQueryResponse> GetShippingMethodsByLocationAsync(string location);

        Task<bool> CheckShippingMethodExistsAsync(string key);

        Task<IShippingMethodPagedQueryResponse> GetShippingMethodsMatchingInStoreCartAsync(string storeKey, string cartId);

    }

    public class ShippingService : IShippingService
    {
        private readonly ProjectApiRoot _api;

        public ShippingService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IShippingMethod> GetShippingMethodByKeyAsync(string key)
        {
            var response = await _api
                .ShippingMethods()
                .WithKey(key)
                .Get()
                .ExecuteAsync();

            return response;
        }

        public async Task<IShippingMethodPagedQueryResponse> GetShippingMethodsAsync()
        {
            var response = await _api
                .ShippingMethods()
                .Get()
                .ExecuteAsync();

            return response;
        }

        public async Task<IShippingMethodPagedQueryResponse> GetShippingMethodsByLocationAsync(string location)
        {
            var response = await _api
                .ShippingMethods()
                .MatchingLocation()
                .Get() // Use the correct method to filter by location
                .WithCountry(location)
                .ExecuteAsync();

            return response;
        }

        public async Task<bool> CheckShippingMethodExistsAsync(string key)
        {
            try
            {
                await _api
                    .ShippingMethods()
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
        
        public async Task<IShippingMethodPagedQueryResponse> GetShippingMethodsMatchingInStoreCartAsync(string storeKey, string cartId)
        {
            return await _api
                .InStore(storeKey)
                .ShippingMethods()
                .MatchingCart()
                .Get() // Use the correct method to filter by cart in store
                .WithCartId(cartId)
                .ExecuteAsync();
        }

    }
}
