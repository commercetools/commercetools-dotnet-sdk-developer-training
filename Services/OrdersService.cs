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
            var order = await _api
                .InStore(storeKey)
                .Orders()
                .Post(new OrderFromCartDraft
                {
                    Cart = new CartResourceIdentifier
                    {
                        Id = request.CartId,
                        TypeId = IReferenceTypeId.Cart
                    },
                    Version = request.CartVersion,
                    OrderNumber = $"CT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
                })
                .ExecuteAsync();
            return order;
        }

        public async Task<IOrder> UpdateOrderCustomFields(string storeKey, string orderNumber, OrderCustomFieldsRequest request)
        {
            var order = await GetOrderByOrderNumberAsync(storeKey, orderNumber);
            return await _api
                .InStore(storeKey)
                .Orders()
                .WithOrderNumber(orderNumber)
                .Post(new OrderUpdate
                    {
                        Actions = new List<IOrderUpdateAction>
                            {
                                new OrderSetCustomTypeAction{
                                    Type = new TypeResourceIdentifier
                                    {
                                        Key = "delivery-instructions",
                                        TypeId = IReferenceTypeId.Type
                                    },
                                    Fields = new FieldContainer
                                    {
                                        { "instructions", request.Instructions },
                                        { "time", request.Time }
                                    }
                                }
                            },
                        Version = order.Version
                    }
                )
                .ExecuteAsync();
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
