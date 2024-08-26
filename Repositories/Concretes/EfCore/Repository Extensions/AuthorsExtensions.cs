using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class AuthorsExtensions
    {
        public static IQueryable<Author> FirstNameAndLastNameSearch(this IQueryable<Author> authors, string searhTerm)
        {
            if (String.IsNullOrWhiteSpace(searhTerm))
                return authors;

            var searchTermWithModified = searhTerm.
                ToLower().
                Trim();

            var results = authors.
                Where(a => a.FirstName.
                ToLower().
                Contains(searhTerm) ||
                a.LastName.
                Contains(searhTerm));

            return results;
        }
    }
}
