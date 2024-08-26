namespace Entities.RequestFeatures
{
    public class CustomerParams : RequestParams
    {
        public string? FirstNameAndLastNameSearchTerm { get; set; }
        public string? EmailSearchTerm { get; set; }

    }
}
