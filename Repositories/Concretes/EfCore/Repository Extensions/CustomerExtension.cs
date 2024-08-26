using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class CustomerExtension
    {
        public static IQueryable<Customer> FirstNameAndLastNameSearch(this IQueryable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return customers;

            var searchTermWithModified = searchTerm.ToLower().Trim();

            var results = customers.
                Where(c => c.FirstName.
                ToLower().
                Contains(searchTerm) ||
                c.LastName.
                ToLower().
                Contains(searchTerm));

            return results;
        }

        public static IQueryable<Customer> EmailSearchTerm(this IQueryable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return customers;
            }

            var searchTermWithModified = searchTerm.ToLower().Trim();

            var results = customers.
                Where(c => c.Email.
                ToLower().
                Contains(searchTerm));

            return results;
        }

    }
}
