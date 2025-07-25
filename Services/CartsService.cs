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
        Task<ICart> CreateCartAsync(string storeKey, CartCreateRequest request);
        Task<ICart> AddLineItemAsync(string storeKey, string cartId, CartUpdateRequest request);
        Task<ICart> AddDiscountCodeAsync(string storeKey, string cartId, CartUpdateCodeRequest request);
        Task<ICart> SetShippingAddressAsync(string storeKey, string cartId, CartUpdateShippingAddressRequest request);
        Task<ICart> SetShippingMethodAsync(string storeKey, string cartId, CartUpdateShippingMethodRequest request);
    }

    public class CartsService : ICartsService
    {
        private readonly ProjectApiRoot _api;
        public CartsService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<ICart> CreateCartAsync(string storeKey, CartCreateRequest request)
        {
            // TODO: Create a cart with anonymousId and add SKU as a line item
            throw new NotImplementedException("This method is not yet implemented.");
        }
        private async Task<ICart> UpdateCartAsync(string storeKey, string cartId, long version, List<ICartUpdateAction> actions) => 
            await _api.InStore(storeKey).Carts().WithId(cartId)
                .Post(new CartUpdate { Version = version, Actions = actions }).ExecuteAsync();
                
        public async Task<ICart> AddLineItemAsync(string storeKey, string cartId, CartUpdateRequest request)
        {
            // TODO: Add another SKU to the cart
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<ICart> AddDiscountCodeAsync(string storeKey, string cartId, CartUpdateCodeRequest request)
        {
            var cart = await GetCartByIdAsync(storeKey, cartId);

            var actions = new List<ICartUpdateAction>
            {
                new CartAddDiscountCodeAction{
                    Code = request.Code,
                }
            };
            return await UpdateCartAsync(storeKey, cartId, cart.Version, actions);
        }

        public async Task<ICart> SetShippingAddressAsync(string storeKey, string cartId, CartUpdateShippingAddressRequest request)
        {
            // TODO: Set Shipping address on the cart
            // TODO: Set email on the cart
            // TODO: Set default Shipping Method (update setShippingMethod, if needed)
            throw new NotImplementedException("This method is not yet implemented.");
        }

        public async Task<ICart> SetShippingMethodAsync(string storeKey, string cartId, CartUpdateShippingMethodRequest request)
        {
            var cart = await GetCartByIdAsync(storeKey, cartId);
            var actions = new List<ICartUpdateAction>
            {
                new CartSetShippingMethodAction{
                    ShippingMethod = new ShippingMethodResourceIdentifier{
                        Id = request.ShippingMethodId,
                        TypeId = IReferenceTypeId.ShippingMethod
                    }
                }
            };
            return await UpdateCartAsync(storeKey, cartId, cart.Version, actions);
        }

        public async Task<ICartPagedQueryResponse> GetCartsAsync(string storeKey) =>
            await _api.InStore(storeKey).Carts().Get().ExecuteAsync();

        public async Task<ICart> GetCartByIdAsync(string storeKey, string id) =>
            await _api.InStore(storeKey).Carts().WithId(id).Get().ExecuteAsync();
    }
}