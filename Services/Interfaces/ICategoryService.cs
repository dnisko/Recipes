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
    }
}
