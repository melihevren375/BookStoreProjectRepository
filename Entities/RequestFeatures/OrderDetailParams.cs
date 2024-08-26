namespace Entities.RequestFeatures
{
    public class OrderDetailParams : RequestParams
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; }
    }
}
