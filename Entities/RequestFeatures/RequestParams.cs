namespace Entities.RequestFeatures
{
    public abstract class RequestParams
    {
        private const int maxPageSize = 10;

        public int PageNumber { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value>maxPageSize ? maxPageSize : value; }
        }

        public string? Fields { get; set; }

    }
}
