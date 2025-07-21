using System.Threading.Tasks;
using commercetools.Sdk.Api.Client;
// using commercetools.Base.Client;
using commercetools.Sdk.Api.Models.Customers;
using Microsoft.AspNetCore.Mvc;
using Training.Services;
using Microsoft.Extensions.Configuration;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using commercetools.Sdk.Api.Client.RequestBuilders.Projects;


namespace Training.Services
{
    public interface ICustomerService
    {
        Task<ICustomer> GetCustomerByIdAsync(string id);
        Task<ICustomer> CreateCustomerAsync(ICustomerDraft customerDraft);
        // Add more customer-related methods as needed
    }

    public class CustomerService : ICustomerService
    {
        private readonly ByProjectKeyRequestBuilder _api;


        public CustomerService(ByProjectKeyRequestBuilder api)

        {
            _api = api;
        }

        public async Task<ICustomer> GetCustomerByIdAsync(string id)
        {
            var response = await _api
                .Customers()
                .WithId(id)
                .Get()
                .ExecuteAsync();

            return response;
        }

        public async Task<ICustomer> CreateCustomerAsync(ICustomerDraft customerDraft)
        {
            var response = await _api
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();

            return response.Customer;
        }
    }
}
