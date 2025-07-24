namespace Training.ViewModels
{
    public class OrderCreateRequest
    {
        public string CartId { get; set; }
        public int CartVersion { get; set; }
        
    }

    public class OrderCustomFieldsRequest
    {
        public string Time { get; set; }
        public string Instructions { get; set; }
    }
    
}