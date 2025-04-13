using Common.Responses;
using DTOs.RecipeDto;

namespace Services.Interfaces
{
    public interface IRecipeService
    {
        Task<CustomResponse<List<RecipeDto>>> GetAllRecipesAsync();
        Task<CustomResponse<List<RecipeDto>>> GetRecipeByIdAsync(int id);
        Task<CustomResponse<List<RecipeDto>>> GetRecipesByCategoryAsync(int categoryId);
        Task<CustomResponse<List<RecipeDto>>> SearchRecipesAsync(string keyword);
        Task<CustomResponse<List<RecipeDto>>> GetRecipesWithTagsAsync();
        Task<CustomResponse<List<RecipeDto>>> GetRecipeDetailsAsync(int recipeId);

        Task<CustomResponse> AddRecipeAsync(RecipeDto recipe);
        Task<CustomResponse> UpdateRecipeAsync(RecipeDto recipe);
        Task<CustomResponse> DeleteRecipeAsync(int id);
    }
}
