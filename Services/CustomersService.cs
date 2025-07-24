using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Customers;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Carts;


namespace Training.Services
{
    public interface ICustomerService
    {
        Task<ICustomer> GetCustomerByIdAsync(string storeKey, string id);
        Task<ICustomerSignInResult> CreateCustomerAsync(string storeKey, CustomerSignupRequest customerSignupRequest);

        Task<ICustomerSignInResult> SigninCustomerAsync(string storeKey, CustomerSigninRequest customerSigninRequest);
        
    }

    public class CustomerService : ICustomerService
    {
        private readonly ProjectApiRoot _api;


        public CustomerService(ProjectApiRoot api)

        {
            _api = api;
        }

        public async Task<ICustomer> GetCustomerByIdAsync(string storeKey, string id)
        {
            return await _api
                 .InStore(storeKey)
                 .Customers()
                 .WithId(id)
                 .Get()
                 .ExecuteAsync();
        }

        public async Task<ICustomerSignInResult> CreateCustomerAsync(string storeKey, CustomerSignupRequest customerSignupRequest)
        {
            var customerDraft = new CustomerDraft
            {
                // TODO: Create customer draft with the anonymous cart
            };
            return await _api
                .InStore(storeKey)
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();

        }

        public async Task<ICustomerSignInResult> SigninCustomerAsync(string storeKey, CustomerSigninRequest customerSigninRequest)
        {
            // TODO: Signin (login) a customer and merge carts
            throw new NotImplementedException("This method is not yet implemented.");
        }
    }
}
