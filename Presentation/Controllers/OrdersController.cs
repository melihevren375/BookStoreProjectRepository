using Entities.DataTransferObjects.OrderDataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpDelete("orders/{orderid:int}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute(Name = "orderid")] int id)
        {
            await _orderService.DeleteOrderAsync(id, true);
            return NoContent();
        }

        [HttpOptions("orders")]
        public IActionResult OptionsOrders()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("orders")]
        public async Task<IActionResult> GetAllOrdersAsyncWithHead([FromQuery] OrderParams orderParams)
        {
            var pagedListResults = await _orderService.GetAllOrdersAsync(orderParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDtoForInsertion orderDtoForInsertion)
        {
            await _orderService.CreateOrderAsync(orderDtoForInsertion);
            return StatusCode(201, orderDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("orders/{id:int}")]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute(Name = "id")] int id, [FromBody] OrderDtoForUpdate orderDtoForUpdate)
        {
            await _orderService.UpdateOrderAsync(id, true, orderDtoForUpdate);
            return NoContent();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderParams orderParams)
        {
            var pagedListResults = await _orderService.GetAllOrdersAsync(orderParams);
            return Ok(pagedListResults.orderDtosForRead);
        }

        [HttpGet("Daily total order count")]
        public async Task<IActionResult> DailyTotalOrderCountAsync()
        {
            var result=await _orderService.DailyTotalOrderCountAsync();
            return Ok(result);
        }

        [HttpGet("Weekly total order count")]
        public async Task<IActionResult> WeeklyTotalOrderCountAsync()
        {
            var result = await _orderService.WeeklyTotalOrderCountAsync();
            return Ok(result);
        }

        [HttpGet("Monthly total order count")]
        public async Task<IActionResult> MonthlyTotalOrderCount()
        {
            var result = await _orderService.MounthlyTotalOrderCountAsync();
            return Ok(result);
        }
    }
}
