namespace Entities.RequestFeatures
{
    public class OrderParams : RequestParams
    {
        public DateTime? MinOrderTime { get; set; }
        public DateTime? MaxOrderTime { get; set; }
        public uint MinTotalAmount { get; set; }
        public uint MaxTotalAmount { get; set; }
    }
}
