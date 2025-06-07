using Common.Responses;
using DTOs.CategoryDto;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        //Category
        Task<CustomResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<CustomResponse<List<CategoryDto>>> GetCategoriesWithTheirRecipesAsync();
        Task<CustomResponse> DeleteCategoryAsync(int id);
        Task<CustomResponse<CategoryDto>> AddCategory(AddCategoryDto categoryDto);
    }
}
