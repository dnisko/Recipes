using Common.Exceptions.CategoryException;
using Common.Exceptions.ServerException;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var response = await _categoryService.GetAllCategoriesAsync();
                return Ok(response);
            }
            catch (CategoryDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetCategoriesWithTheirRecipesAsync")]
        public async Task<IActionResult> GetCategoriesWithTheirRecipesAsync()
        {
            try
            {
                var response = await _categoryService.GetCategoriesWithTheirRecipesAsync();
                return Ok(response);
            }
            catch (CategoryDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var response = await _categoryService.DeleteCategoryAsync(id);
                return Ok(response);
            }
            catch (CategoryDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
    }
}
