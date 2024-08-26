using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.RequestFeatures;
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

        [HttpDelete("orderDetails/{orderdetailid:int}")]
        public async Task<IActionResult> DeleteOrderDetailAsync([FromRoute(Name = "orderdetailid")] int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id, true);
            return NoContent();
        }

        [HttpOptions("orderDetails")]
        public IActionResult OptionsOrderDetails()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("orderDetails")]
        public async Task<IActionResult> GetAllOrderDetailsAsyncWithHead([FromQuery] OrderDetailParams orderDetailParams)
        {
            var pagedListResults = await _orderDetailService.GetAllOrderDetailsAsync(orderDetailParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("orderDetails")]
        public async Task<IActionResult> CreateOrderDetailAsync([FromBody] OrderDetailForInsertion orderDetailForInsertion)
        {
            await _orderDetailService.CreateOrderDetailAsync(orderDetailForInsertion);
            return StatusCode(201, orderDetailForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("orderDetails/{id:int}")]
        public async Task<IActionResult> UpdateOrderDetailAsync([FromRoute(Name = "id")] int id, [FromBody] OrderDetailForUpdate orderDetailForUpdate)
        {
            await _orderDetailService.UpdateOrderDetailAsync(id, true, orderDetailForUpdate);
            return NoContent();
        }

        [HttpGet("orderDetails")]
        public async Task<IActionResult> GetAllOrderDetails([FromQuery] OrderDetailParams orderDetailParams)
        {
            var pagedListResults = await _orderDetailService.GetAllOrderDetailsAsync(orderDetailParams);
            return Ok(pagedListResults.orderDetailDtosForRead);
        }

        [HttpGet("orderDetailsWithDetails")]
        public async Task<IActionResult> GetOrderDetailsWithDetailsAsync([FromQuery] OrderDetailParams orderDetailParams)
        {
            var results = await _orderDetailService.GetAllOrderDetailsWithDetailsAsync(orderDetailParams);
            return Ok(results.orderDetailDtosForDetails);
        }
    }
}
