using Entities.DataTransferObjects.EmailCodeDataTransferObjects;

namespace Services.Contracts;

public interface IEmailCodeService
{
    Task<EmailCodeDtoForRead> GetEmailCodeByCustomerIdAsync(Guid id, bool trackChanges);

    Task CreateEmailCodeAsync(EmailCodeFtoForInsertion emailCodeFtoForInsertion);
}
