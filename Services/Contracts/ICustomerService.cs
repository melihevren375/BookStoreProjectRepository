using Entities.DataTransferObjects.CustomerDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        Task<(IEnumerable<ExpandoObject> customerDtosForRead, MetaData metaData)> GetAllCustomersAsync(CustomerParams customerParams);
        Task DeleteCustomerAsync(Guid id, bool trackChanges);
        Task UpdateCustomerAsync(Guid id, bool trackChanges, CustomerDtoForUpdate customerDtoForUpdate);
        Task CreateCustomerAsync(CustomerDtoForInsertion customerDtoForInsertion);
        Task<bool> SignInControlAsync(CustomerDtoForSignInControl customerDtoForSign);
        Task<bool> EmailControlAsync(string email);
        Task ChangedPasswordAsync(CustomerDtoForChangePassword customerDtoForChangePassword, bool trackChanges);
    }
}