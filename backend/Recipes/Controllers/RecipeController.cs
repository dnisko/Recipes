using Common.Exceptions.IngredientException;
using Common.Exceptions.RecipeException;
using Common.Exceptions.ServerException;
using Common.Exceptions.TagException;
using Common.Responses;
using DTOs;
using DTOs.RecipeDto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet("getAllWithDetails")]
        public async Task<IActionResult> GetAllRecipes([FromQuery] RecipePaginationParams paginationParams)
        {
            var response = await _recipeService.GetAllRecipesAsync(paginationParams);
            return Response(response);
        }
        [HttpGet("getAllWithDetails1")]
        public async Task<IActionResult> GetAllRecipesDetails(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _recipeService.GetAllRecipesDetailsAsync(pageNumber, pageSize);
                return Response<PaginatedResult<RecipeDto>>(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            try
            {
                var response = await _recipeService.GetRecipeByIdAsync(id);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getByCategory/{categoryId}")]
        public async Task<IActionResult> GetRecipesByCategory(int categoryId)
        {
            try
            {
                var response = await _recipeService.GetRecipesByCategoryAsync(categoryId);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipes(string keyword)
        {
            try
            {
                var response = await _recipeService.SearchRecipesAsync(keyword);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRecipe([FromBody] AddRecipeDto recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var response = await _recipeService.AddRecipeAsync(recipe);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeDto recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var response = await _recipeService.UpdateRecipeAsync(recipe);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                var response = await _recipeService.DeleteRecipeAsync(id);
                return Response(response);
            }
            catch (RecipeDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Ingredient
        [HttpGet("GetIngredientsByRecipe/{recipeId}")]
        public async Task<IActionResult> GetIngredientsByRecipe(int recipeId)
        {
            try
            {
                var response = await _recipeService.GetIngredientsByRecipe(recipeId);
                return Response(response);
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Tags
        [HttpGet("GetPopularTags")]
        public async Task<IActionResult> GetPopularTags()
        {
            try
            {
                var response = await _recipeService.GetPopularTagsAsync();
                return Response(response);
            }
            catch (TagDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetTagsByARecipe/{id}")]
        public async Task<IActionResult> GetTagsByARecipe(int id)
        {
            try
            {
                var response = await _recipeService.GetTagsByARecipeAsync(id);
                return Response(response);
            }
            catch (TagDataException ex)
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
