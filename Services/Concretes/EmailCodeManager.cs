using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.EmailCodeDataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concretes;

public class EmailCodeManager : IEmailCodeService
{
    private readonly IEmailCodeRepository _emailCodeRepository;
    private readonly IMapper _mapper;

    public EmailCodeManager(IEmailCodeRepository emailCodeRepository, IMapper mapper)
    {
        _emailCodeRepository = emailCodeRepository;
        _mapper = mapper;
    }

    public async Task CreateEmailCodeAsync(EmailCodeFtoForInsertion emailCodeFtoForInsertion)
    {
        var emailCode = _mapper.Map<EmailCode>(emailCodeFtoForInsertion);

        _emailCodeRepository.CreateEmailCode(emailCode);
    }

    public async Task<EmailCodeDtoForRead> GetEmailCodeByCustomerIdAsync(Guid id, bool trackChanges)
    {
        var emailCode = await _emailCodeRepository.
            GetEmailCodeByConditionAsync(ec => ec.CustomerId.Equals(id), trackChanges);

        var emailCodeDtoForRead = _mapper.Map<EmailCodeDtoForRead>(emailCode);

        return emailCodeDtoForRead;
    }
}
