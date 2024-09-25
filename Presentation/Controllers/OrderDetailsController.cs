using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/orderdetails")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpDelete("DeleteOrderDetailAsync/{orderdetailid:Guid}")]
        public async Task<IActionResult> DeleteOrderDetailAsync([FromRoute(Name = "orderdetailid")] Guid id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id, true);
            return NoContent();
        }

        [HttpOptions("OptionsOrderDetails")]
        public IActionResult OptionsOrderDetails()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("GetAllOrderDetailsAsyncWithHead")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<IActionResult> GetAllOrderDetailsAsyncWithHead([FromQuery] OrderDetailParams orderDetailParams)
        {
            var pagedListResults = await _orderDetailService.GetAllOrderDetailsAsync(orderDetailParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("CreateOrderDetailAsync")]
        public async Task<IActionResult> CreateOrderDetailAsync([FromBody] OrderDetailForInsertion orderDetailForInsertion)
        {
            await _orderDetailService.CreateOrderDetailAsync(orderDetailForInsertion);
            return StatusCode(201, orderDetailForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("UpdateOrderDetailAsync/{id:Guid}")]
        public async Task<IActionResult> UpdateOrderDetailAsync([FromRoute(Name = "id")] Guid id, [FromBody] OrderDetailForUpdate orderDetailForUpdate)
        {
            await _orderDetailService.UpdateOrderDetailAsync(id, true, orderDetailForUpdate);
            return NoContent();
        }

        [HttpGet("GetAllOrderDetails")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<IActionResult> GetAllOrderDetails([FromQuery] OrderDetailParams orderDetailParams)
        {
            var pagedListResults = await _orderDetailService.GetAllOrderDetailsAsync(orderDetailParams);
            return Ok(pagedListResults.orderDetailDtosForRead);
        }

        [HttpGet("GetOrderDetailsWithDetailsAsync")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<IActionResult> GetOrderDetailsWithDetailsAsync([FromQuery] OrderDetailParams orderDetailParams)
        {
            var results = await _orderDetailService.GetAllOrderDetailsWithDetailsAsync(orderDetailParams);
            return Ok(results.orderDetailDtosForDetails);
        }
    }
}
