using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/emailCodes")]
[ServiceFilter(typeof(LogFilterAttribute))]
public class EmailCodesController:ControllerBase
{
    private readonly IEmailCodeService _emailCodeService;

    public EmailCodesController(IEmailCodeService emailCodeService)
    {
        _emailCodeService = emailCodeService;
    }

    [HttpGet("GetEmailCodeAsync/{id:Guid}")]  
    public async Task<IActionResult> GetEmailCodeAsync([FromRoute(Name ="id")] Guid id)
    {
        var emailCode = await _emailCodeService.
            GetEmailCodeByCustomerIdAsync(id,false);

        return Ok(emailCode);
    }
}
