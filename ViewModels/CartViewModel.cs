namespace Training.ViewModels
{
    public class CartCreateRequest
    {
        // public string SessionId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }

    public class CartUpdateRequest
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }

    public class CartUpdateCodeRequest
    {
        public string Code { get; set; }
    }
    
    public class CartUpdateShippingMethodRequest
    {
        public string ShippingMethodId { get; set; }  
    }
    
    public class CartUpdateShippingAddressRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
    
}