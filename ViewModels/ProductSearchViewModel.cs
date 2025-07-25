namespace Training.ViewModels
{
    public class SearchRequest
    {
        public string Locale { get; set;}
        public string Keyword { get; set; }
        public string StoreKey { get; set; }
        public bool IncludeFacets { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }   
    }
}