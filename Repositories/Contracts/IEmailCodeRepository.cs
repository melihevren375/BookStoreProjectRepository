using Entities.Concretes;
using System.Linq.Expressions;

namespace Repositories.Contracts;

public interface IEmailCodeRepository : IBaseRepository<EmailCode>
{
    Task<EmailCode> GetEmailCodeByConditionAsync(Expression<Func<EmailCode, bool>> expression, bool trackChanges);
    void CreateEmailCode(EmailCode emailCode);
    Task<bool> ControlEmailCodeByCodeAsync(int code);
    Task<bool> ControlEmailCodeByCustomerAsync(Guid customerId);
    Task<bool> ControlEmailCodeByCustomerAndCodeAsync(Guid customerId,int code);
    void UpdateEmailCode(EmailCode emailCode);
}
