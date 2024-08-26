using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore;

public class EmailCodeRepository : BaseRepository<EmailCode>, IEmailCodeRepository
{
    public EmailCodeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<bool> ControlEmailCodeByCodeAsync(int code)
    {
        var result = await GetAllEntities().
            AnyAsync(ec => ec.Code.Equals(code));

        return result;
    }

    public async Task<bool> ControlEmailCodeByCustomerAndCodeAsync(Guid customerId, int code)
    {
        var result = await GetAllEntities().
            AnyAsync(ec => ec.Code.Equals(code) && ec.CustomerId.Equals(customerId));

        return result;
    }

    public async Task<bool> ControlEmailCodeByCustomerAsync(Guid customerId)
    {
        var result = await GetAllEntities().
            AnyAsync(ec => ec.CustomerId.Equals(customerId));

        return result;
    }

    public void CreateEmailCode(EmailCode emailCode) => CreateEntity(emailCode);

    public async Task<EmailCode> GetEmailCodeByConditionAsync(Expression<Func<EmailCode, bool>> expression, bool trackChanges)
    {
        var emailCode = !trackChanges ?
           await GetEntitiesByCondition(trackChanges, expression).
            AsNoTracking().
            SingleOrDefaultAsync() :
          await GetEntitiesByCondition(trackChanges, expression).
            SingleOrDefaultAsync();

        return emailCode;
    }

    public void UpdateEmailCode(EmailCode emailCode) => UpdateEntity(emailCode);
}
