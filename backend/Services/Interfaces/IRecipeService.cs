using Common.Responses;
using DTOs.IngredientDto;
using DTOs.RecipeDto;
using DTOs.TagDto;

namespace Services.Interfaces
{
    public interface IRecipeService
    {
        Task<CustomResponse<List<RecipeDto>>> GetAllRecipesAsync();
        Task<CustomResponse<RecipeDto>> GetRecipeByIdAsync(int id);
        Task<CustomResponse<List<RecipeDto>>> GetRecipesByCategoryAsync(int categoryId);
        Task<CustomResponse<List<RecipeDto>>> SearchRecipesAsync(string keyword);
        //Task<CustomResponse<List<RecipeDto>>> GetRecipesWithTagsAsync();
        //Task<CustomResponse<List<RecipeDto>>> GetRecipeDetailsAsync(int recipeId);

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
