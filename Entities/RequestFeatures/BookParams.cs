namespace Entities.RequestFeatures
{
    public class BookParams:RequestParams
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; }
        public bool ValidPrice => MaxPrice > MinPrice;
        public string? SearchTerm { get; set; }
    }
}
