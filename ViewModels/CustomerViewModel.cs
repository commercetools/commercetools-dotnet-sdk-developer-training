namespace Training.ViewModels
{
    public class CustomerSignupRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public bool IsDefaultShippingAddress { get; set; }
        public bool IsDefaultBillingAddress { get; set; }
        public string AnonymousCartId { get; set; }

    }
    
    public class CustomerSigninRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AnonymousCartId { get; set; }
        
    }
}