using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class PublisherExtensions
    {
        public static IQueryable<Publisher> SearchName(this IQueryable<Publisher> publishers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return publishers;

            var searchTermWithModified = searchTerm.
                ToLower().
                Trim();

            var results = publishers.
                Where(p => p.Name.
                ToLower().
                Contains(searchTerm));

            return results;
        }
    }
}
