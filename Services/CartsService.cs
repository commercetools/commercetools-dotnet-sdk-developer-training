using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Carts;
using Training.ViewModels;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.ShippingMethods;


namespace Training.Services
{
    public interface ICartsService
    {
        Task<ICartPagedQueryResponse> GetCartsAsync(string storeKey);
        Task<ICart> GetCartByIdAsync(string storeKey, string id);
        Task<ICart> CreateCart(string storeKey, CartCreateRequest request);
        Task<ICart> UpdateCart(string storeKey, string cartId, CartUpdateRequest request);
        Task<ICart> UpdateCartCode(string storeKey, string cartId, CartUpdateCodeRequest request);
        Task<ICart> UpdateCartShippingAddress(string storeKey, string cartId, CartUpdateShippingAddressRequest request);
        Task<ICart> UpdateCartShippingMethod(string storeKey, string cartId, CartUpdateShippingMethodRequest request);

    }

    public class CartsService : ICartsService
    {
        private readonly ProjectApiRoot _api;

        public CartsService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<ICart> CreateCart(string storeKey, CartCreateRequest request)
        {
            // TODO: Create a cart with anonymousId and add SKU as a line item
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<ICart> UpdateCart(string storeKey, string cartId, CartUpdateRequest request)
        {
            // TODO: Add another SKU to the cart
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<ICart> UpdateCartCode(string storeKey, string cartId, CartUpdateCodeRequest request)
        {
            var cart = await GetCartByIdAsync(storeKey, cartId);
            return await _api
                .InStore(storeKey)
                .Carts()
                .WithId(cartId)
                .Post(new CartUpdate
                    {
                        Actions = new List<ICartUpdateAction>
                            {
                                new CartAddDiscountCodeAction{
                                    Code = request.Code,
                                }
                            },
                        Version = cart.Version
                    }
                )
                .ExecuteAsync();
        }

        public async Task<ICart> UpdateCartShippingAddress(string storeKey, string cartId, CartUpdateShippingAddressRequest request)
        {
            // TODO: Set Shipping address on the cart
            // TODO: Set email on the cart
            // TODO: Set default Shipping Method (update setShippingMethod, if needed)
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<ICart> UpdateCartShippingMethod(string storeKey, string cartId, CartUpdateShippingMethodRequest request)
        {
            var cart = await GetCartByIdAsync(storeKey, cartId);
            return await _api
                .InStore(storeKey)
                .Carts()
                .WithId(cartId)
                .Post(new CartUpdate
                    {
                        Actions = new List<ICartUpdateAction>
                            {
                                new CartSetShippingMethodAction{
                                    ShippingMethod = new ShippingMethodResourceIdentifier{
                                        Id = request.ShippingMethodId,
                                        TypeId = IReferenceTypeId.ShippingMethod
                                    }
                                }
                            },
                        Version = cart.Version
                    }
                )
                .ExecuteAsync();
        }

        public async Task<ICartPagedQueryResponse> GetCartsAsync(string storeKey)
        {
            return await _api
                .InStore(storeKey)
                .Carts()
                .Get()
                .ExecuteAsync();
        }

        public async Task<ICart> GetCartByIdAsync(string storeKey, string id)
        {
            return await _api
                .InStore(storeKey)
                .Carts()
                .WithId(id)
                .Get()
                .ExecuteAsync();
        }
        

    }
}
