using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class CategoryExtensions
    {
        public static IQueryable<Category> Search(this IQueryable<Category> categories, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return categories;

            var searchTermWithModified = searchTerm.
                ToLower().
                Trim();

            var results = categories.
                Where(c => c.Name.ToLower().Contains(searchTermWithModified));

            return results;
        }
    }
}
