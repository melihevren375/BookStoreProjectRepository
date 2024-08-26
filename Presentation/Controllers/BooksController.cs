using Entities.DataTransferObjects;
using Entities.DataTransferObjects.BookDataTransferObjects;
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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpDelete("{bookid:int}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute(Name = "bookid")] int id)
        {
            await _bookService.DeleteBookAsync(id, true);
            return NoContent();
        }

        [HttpOptions("books")]
        public IActionResult OptionsBooks()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("books")]
        public async Task<IActionResult> GetAllBooksAsyncWithHead([FromQuery] BookParams bookParams )
        {
            var pagedListResults= await _bookService.GetAllBooksWithParamsAsync(bookParams);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("books")]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDtoForInsertion bookDtoForInsertion)
        {
            await _bookService.CreateBookAsync(bookDtoForInsertion);
            return StatusCode(201, bookDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("books/{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute(Name ="id")]int id, [FromBody]BookDtoForUpdate bookDtoForUpdate)
        {
            await _bookService.UpdateBookAsync(id,true,bookDtoForUpdate);
            return NoContent();
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks([FromQuery] BookParams bookParams)
        {
            var pagedListResults = await _bookService.GetAllBooksWithParamsAsync(bookParams);
            return Ok(pagedListResults.bookDtosForRead);        
        }

        [HttpGet("booksWithDetails")]
        public async Task<IActionResult> GetBooksWithDetailsAsync([FromQuery] BookParams bookParams)
        {
            var results=await _bookService.GetBooksWithDetailsAsync(bookParams);
            return Ok(results.bookDtosForRead);
        }

        [HttpGet("GetTheMostPopularBooks")]
        public async Task<IActionResult> GetTheMostPopularBooks()
        {
            var results = await _bookService.GetTheMostPopularBooksAsync();

            return Ok(results);
        }

    }
}
