using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<PagedList<Customer>> GetAllCustomerAsync(CustomerParams customerParams);
        Task<Customer> GetCustomerByConditionAsync(bool trackChanges, Expression<Func<Customer, bool>> expression);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        bool SignInControl(Customer customer);
        Task<bool> EmailControlAsync(string email);
    }
}
