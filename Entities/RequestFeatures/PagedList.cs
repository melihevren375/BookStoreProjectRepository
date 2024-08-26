namespace Entities.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> values, int count, int pageSize, int pageNumber)
        {
            MetaData = new MetaData()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = count,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(values);
        }
        public MetaData MetaData { get; set; }

        public static PagedList<T> ToPagedList(IEnumerable<T> values, int pageSize, int pageNumber)
        {
            var entites = values.
                Skip((pageNumber - 1) * pageSize).
                Take(pageSize).
                ToList();

            int count = entites.Count();

            return new PagedList<T>(entites, count, pageSize, pageNumber);
        }
    }
}
