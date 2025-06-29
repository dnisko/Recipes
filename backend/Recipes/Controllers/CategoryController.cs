using Common.Exceptions.CategoryException;
using Common.Exceptions.ServerException;
using DTOs;
using DTOs.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var response = await _categoryService.GetAllCategoriesAsync(paginationParams);
                return Response(response);
                //return BadRequest("not implemented");
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
        public async Task<IActionResult> GetCategoriesWithTheirRecipesAsync([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var response = await _categoryService.GetCategoriesWithTheirRecipesAsync(paginationParams);
                return Response(response);
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
                return Response(response);
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
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        {
            try
            {
                var response = await _categoryService.AddCategory(categoryDto);
                return Response(response);
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
