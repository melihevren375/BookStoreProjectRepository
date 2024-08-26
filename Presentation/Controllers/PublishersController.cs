using Entities.DataTransferObjects.PublisherDataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpDelete("publishers/{publisherid:int}")]
        public async Task<IActionResult> DeletePublisherAsync([FromRoute(Name = "publisherid")] int id)
        {
            await _publisherService.DeletePublisherAsync(id, true);
            return NoContent();
        }

        [HttpOptions("publishers")]
        public IActionResult OptionsPublishers()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("publishers")]
        public async Task<IActionResult> GetAllPublishersAsyncWithHead([FromQuery] PublisherParams publisherParams)
        {
            var pagedListResults = await _publisherService.GetAllPublishersAsync(publisherParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("publishers")]
        public async Task<IActionResult> CreatePublisherAsync([FromBody] PublisherDtoForInsertion publisherDtoForInsertion)
        {
            await _publisherService.CreatePublisherAsync(publisherDtoForInsertion);
            return StatusCode(201, publisherDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("publihers/{id:int}")]
        public async Task<IActionResult> UpdatePublisherAsync([FromRoute(Name = "id")] int id, [FromBody] PublisherDtoForUpdate publisherDtoForUpdate)
        {
            await _publisherService.UpdatePublisherAsync(id, true, publisherDtoForUpdate);
            return NoContent();
        }

        [HttpGet("publishers")]
        public async Task<IActionResult> GetAllPublishers([FromQuery] PublisherParams publisherParams)
        {
            var pagedListResults = await _publisherService.GetAllPublishersAsync(publisherParams);
            return Ok(pagedListResults.publiherDtosRead);
        }

    }
}
