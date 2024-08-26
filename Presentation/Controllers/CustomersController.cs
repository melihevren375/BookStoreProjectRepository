using Entities.DataTransferObjects.CustomerDataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpDelete("customers/{id:int}")]
        public async Task<IActionResult> DeleteCustomerAsync([FromRoute(Name = "id")] int id)
        {
            await _customerService.DeleteCustomerAsync(id, true);
            return NoContent();
        }

        [HttpOptions("customers")]
        public IActionResult OptionsCustomers()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("customers")]
        public async Task<IActionResult> GetAllCustomersAsyncWithHead([FromQuery] CustomerParams customerParams)
        {
            var pagedListResults = await _customerService.GetAllCustomersAsync(customerParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("customers")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDtoForInsertion customerDtoForInsertion)
        {
            await _customerService.CreateCustomerAsync(customerDtoForInsertion);
            return StatusCode(201, customerDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("customers/{id:int}")]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute(Name = "id")] int id, [FromBody] CustomerDtoForUpdate customerDtoForUpdate)
        {
            await _customerService.UpdateCustomerAsync(id, true, customerDtoForUpdate);
            return NoContent();
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] CustomerParams customerParams)
        {
            var pagedListResults = await _customerService.GetAllCustomersAsync(customerParams);
            return Ok(pagedListResults.customerDtosForRead);
        }

        [HttpGet("customerSignInControl")]
        public async Task<IActionResult> SignInControlAsync([FromQuery] CustomerDtoForSignInControl customerDtoForSignInControl)
        {
            var result = await _customerService.SignInControlAsync(customerDtoForSignInControl);
            return Ok(result);
        }

        [HttpGet("customerss")]
        public async Task<IActionResult> EmailControlAsync([FromQuery] string email)
        {
            var result = _customerService.EmailControlAsync(email).Result;

            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] CustomerDtoForChangePassword customerDtoForChangePassword)
        {
            await _customerService.ChangedPasswordAsync(customerDtoForChangePassword, true);
            return NoContent();
        }
    }
}
