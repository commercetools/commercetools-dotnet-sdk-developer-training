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
            // TODO: Return a shipping method by key
            throw new NotImplementedException("This method is not yet implemented.");
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
            // TODO: Return a list of shipping methods valid for a country
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<bool> CheckShippingMethodExistsAsync(string key)
        {
            // TODO: Return true if a shipping method by key exists
            throw new NotImplementedException("This method is not yet implemented.");
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
