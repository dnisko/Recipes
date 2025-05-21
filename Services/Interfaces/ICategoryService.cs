using Common.Responses;
using DTOs.CategoryDto;
using DTOs.IngredientDto;
using DTOs.TagDto;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        //Category
        Task<CustomResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<CustomResponse<List<CategoryDto>>> GetCategoriesWithTheirRecipesAsync();
        Task<CustomResponse> DeleteCategoryAsync(int id);

        //Ingredient
        Task<CustomResponse<List<IngredientDto>>> GetIngredientsByRecipe(int recipeId);

        //Tag
        Task<CustomResponse<List<TagDto>>> GetPopularTagsAsync();
        Task<CustomResponse<List<TagDto>>> GetTagsByARecipeAsync(int id);
    }
}
