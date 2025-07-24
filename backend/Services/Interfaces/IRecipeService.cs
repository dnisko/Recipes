using Common.Responses;
using DTOs;
using DTOs.IngredientDto;
using DTOs.RecipeDto;
using DTOs.TagDto;

namespace Services.Interfaces
{
    public interface IRecipeService
    {
        //Task<CustomResponse<PaginatedResult<RecipeDto>>> GetAllRecipesDetailsAsync(int pageNumber, int pageSize);
        Task<CustomResponse<PaginatedResult<RecipeDto>>> GetAllRecipesAsync(RecipePaginationParams paginationParams);
        Task<CustomResponse<RecipeDto>> GetRecipeByIdAsync(int id);
        Task<CustomResponse<PaginatedResult<RecipeDto>>> GetRecipesByCategoryAsync(int categoryId);
        Task<CustomResponse<PaginatedResult<RecipeDto>>> SearchRecipesAsync(string keyword);
        Task<CustomResponse> AddRecipeAsync(AddRecipeDto recipe);
        Task<CustomResponse> UpdateRecipeAsync(UpdateRecipeDto recipe);
        Task<CustomResponse> DeleteRecipeAsync(int id);

        //Ingredient
        Task<CustomResponse<PaginatedResult<IngredientDto>>> GetIngredientsByRecipe(int recipeId);

        //Tag
        Task<CustomResponse<PaginatedResult<TagDto>>> GetPopularTagsAsync();
        Task<CustomResponse<PaginatedResult<TagDto>>> GetTagsByARecipeAsync(int id);
    }
}
