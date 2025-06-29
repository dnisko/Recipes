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
        Task<CustomResponse<List<RecipeDto>>> GetRecipesByCategoryAsync(int categoryId);
        Task<CustomResponse<List<RecipeDto>>> SearchRecipesAsync(string keyword);
        Task<CustomResponse> AddRecipeAsync(AddRecipeDto recipe);
        Task<CustomResponse> UpdateRecipeAsync(UpdateRecipeDto recipe);
        Task<CustomResponse> DeleteRecipeAsync(int id);

        //Ingredient
        Task<CustomResponse<List<IngredientDto>>> GetIngredientsByRecipe(int recipeId);

        //Tag
        Task<CustomResponse<List<TagDto>>> GetPopularTagsAsync();
        Task<CustomResponse<List<TagDto>>> GetTagsByARecipeAsync(int id);
    }
}
