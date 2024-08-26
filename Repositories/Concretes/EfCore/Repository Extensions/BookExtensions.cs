using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Extensions
{
    public static class BookExtensions
    {
        public static IQueryable<Book> FilterPrice(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            if (maxPrice == 0)
                maxPrice = uint.MaxValue;

            var values = books.
                Where(b => (b.Price < maxPrice)
                &&
                (b.Price > minPrice));

            return values;
        }

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var values = books.
                Where(b => b.Title.ToLower().Contains(searchTerm.Trim().ToLower()));

            return values;
        }
    }
}
