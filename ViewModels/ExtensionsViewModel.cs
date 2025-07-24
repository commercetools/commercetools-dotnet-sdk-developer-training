namespace Training.ViewModels
{
    public class CustomObjectCreateRequest
    {
        public Subscriber Subscriber { get; set; }
        public string Key { get; set; }
        public string Container { get; set; }

    }
    
    public class Subscriber
    {
        public string Email { get; set; }
        public string Name { get; set; }
        
    }
    
}