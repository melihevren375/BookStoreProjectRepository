using Entities.DataTransferObjects.CategoryDataTransferObjects;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpDelete("DeleteCategoryAsync/{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute(Name = "id")] Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id, true);
            return NoContent();
        }

        [HttpOptions("OptionsCategories")]
        public IActionResult OptionsCategories()
        {
            Response.Headers.Add("Allow", "GET,PUT,HEAD,DELETE,POST");
            return Ok();
        }

        [HttpHead("GetAllCategoriesAsyncWithHead")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<IActionResult> GetAllCategoriesAsyncWithHead([FromQuery] CategoryParams categoryParams)
        {
            var pagedListResults = await _categoryService.GetAllCategoriesAsync(categoryParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedListResults.metaData));
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("CreateCategoryAsync")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDtoForInsertion categoryDtoForInsertion)
        {
            await _categoryService.CreateCategoryAsync(categoryDtoForInsertion);
            return StatusCode(201, categoryDtoForInsertion);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("UpdateCategoryAsync/{id:Guid}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute(Name = "id")] Guid id, [FromBody] CategoryDtoForUpdate categoryDtoForUpdate)
        {
            await _categoryService.UpdateCategoryAsync(id, true, categoryDtoForUpdate);
            return NoContent();
        }

        [HttpGet("GetAllCategories")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        [HttpCacheValidation(MustRevalidate = true)]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryParams categoryParams)
        {
            var pagedListResults = await _categoryService.GetAllCategoriesAsync(categoryParams);
            return Ok(pagedListResults.categoryDtosForRead);
        }

    }
}
