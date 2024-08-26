namespace Entities.RequestFeatures
{
    public class MetaData
    {
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPage;

    }
}
