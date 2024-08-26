using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCustomer(Customer customer) => CreateEntity(customer);

        public void DeleteCustomer(Customer customer) => DeleteEntity(customer);

        public async Task<bool> EmailControlAsync(string email)
        {
            var result = _repositoryContext.
                Customers.
                AnyAsync(c=>c.Email.Equals(email)).
                Result;

            return result;
        }

        public async Task<PagedList<Customer>> GetAllCustomerAsync(CustomerParams customerParams)
        {
            var customers = await GetAllEntities().
                EmailSearchTerm(customerParams.EmailSearchTerm).
                FirstNameAndLastNameSearch(customerParams.FirstNameAndLastNameSearchTerm).
                ToListAsync();

            return PagedList<Customer>.ToPagedList(customers, customerParams.PageSize, customerParams.PageNumber);
        }

        public async Task<Customer> GetCustomerByConditionAsync(bool trackChanges, Expression<Func<Customer, bool>> expression)
        {
            var customer = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return customer;
        }

        public bool SignInControl(Customer customer)
        {
            var result = _repositoryContext.
                Customers.
                Any(b => b.Email.Equals(customer.Email) && b.Password.Equals(customer.Password));

            return result;
        }

        public void UpdateCustomer(Customer customer) => UpdateEntity(customer);

    }
}
