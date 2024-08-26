using Entities.DataTransferObjects.AutherDataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [Route("api/authors")]
    [ApiController]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthorsAsync([FromQuery] AuthorParams authorParams)
        {
            var pagedListReults = await _authorService.GetAllAuthorsAsync(authorParams);
            var metaDatas = pagedListReults.metaData;
            var authorDtos = pagedListReults.authorDtosForRead;
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaDatas));
            return Ok(authorDtos);
        }

        [HttpDelete("authors/{id:int}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute(Name = "id")] int id)
        {
            await _authorService.DeleteAuthorAsync(id, true);
            return NoContent();
        }

        [HttpHead("authors")]
        public async Task<IActionResult> GetAuthorsWithHead([FromQuery] AuthorParams authorParams)
        {
            var pagedListResults = await _authorService.GetAllAuthorsAsync(authorParams);
            var metaData = pagedListResults.metaData;
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(metaData);
        }

        [HttpOptions("authors")]
        public IActionResult OptionsAuthors()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,OPTIONS,DELETE,POST");
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("authors")]
        public async Task<IActionResult> CreateAuthor([FromBody] AutherDtoForInsertion autherDtoForInsertion)
        {
            await _authorService.CreateAuthorAsync(autherDtoForInsertion);
            return StatusCode(201, autherDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("authors/{id:int}")]
        public async Task<IActionResult> UpdateAuthorAsync([FromRoute(Name = "id")] int id, [FromBody] AutherDtoForUpdate autherDtoForUpdate)
        {
            await _authorService.UpdateAuthorAsync(id, true, autherDtoForUpdate);
            return NoContent();
        }
    }
}
