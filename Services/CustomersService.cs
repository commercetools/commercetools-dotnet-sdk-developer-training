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
                Email = customerSignupRequest.Email,
                Password = customerSignupRequest.Password,
                FirstName = customerSignupRequest.FirstName,
                LastName = customerSignupRequest.LastName,
                Key = customerSignupRequest.Key,
                Addresses = new List<IBaseAddress> { new BaseAddress{
                    Country = customerSignupRequest.Country,
                }},
                DefaultBillingAddress = customerSignupRequest.IsDefaultBillingAddress ? 0 : null,
                DefaultShippingAddress = customerSignupRequest.IsDefaultShippingAddress ? 0 : null,
                AnonymousCart = new CartResourceIdentifier
                {
                    Id = customerSignupRequest.AnonymousCartId,
                    TypeId = IReferenceTypeId.Cart
                }
            };
            return await _api
                .InStore(storeKey)
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();

        }

        public async Task<ICustomerSignInResult> SigninCustomerAsync(string storeKey, CustomerSigninRequest customerSigninRequest)
        {
            var customerSignin = new CustomerSignin
            {
                Email = customerSigninRequest.Email,
                Password = customerSigninRequest.Password,
                AnonymousCart = new CartResourceIdentifier
                {
                    Id = customerSigninRequest.AnonymousCartId,
                    TypeId = IReferenceTypeId.Cart
                }
            };
            return await _api
                .InStore(storeKey)
                .Login()
                .Post(customerSignin)
                .ExecuteAsync();
        }
    }
}
