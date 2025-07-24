using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Carts;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Orders;
using commercetools.Sdk.Api.Models.Types;


namespace Training.Services
{
    public interface IOrdersService
    {
        Task<IOrder> GetOrderByOrderNumberAsync(string storeKey, string orderNumber);
        Task<IOrderPagedQueryResponse> GetOrdersAsync(string storeKey);
        Task<IOrder> CreateOrder(string storeKey, OrderCreateRequest request);
        Task<IOrder> UpdateOrderCustomFields(string storeKey, string orderNumber, OrderCustomFieldsRequest request);

    }

    public class OrdersService : IOrdersService
    {
        private readonly ProjectApiRoot _api;

        public OrdersService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<IOrder> CreateOrder(string storeKey, OrderCreateRequest request)
        {
            // TODO: Create an order with a random order number
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<IOrder> UpdateOrderCustomFields(string storeKey, string orderNumber, OrderCustomFieldsRequest request)
        {
            // TODO: Update the order with custom type and fields
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<IOrderPagedQueryResponse> GetOrdersAsync(string storeKey)
        {
            return await _api
                .InStore(storeKey)
                .Orders()
                .Get()
                .ExecuteAsync();
        }

        public async Task<IOrder> GetOrderByOrderNumberAsync(string storeKey, string orderNumber)
        {
            return await _api
                .InStore(storeKey)
                .Orders()
                .WithOrderNumber(orderNumber)
                .Get()
                .ExecuteAsync();
        }
        

    }
}
