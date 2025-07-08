using Common.Responses;
using DTOs;
using DTOs.CategoryDto;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        //Category
        Task<CustomResponse<PaginatedResult<CategorySimpleDto>>> GetAllCategoriesAsync(CategoryPaginationParams paginationParams);
        Task<CustomResponse<PaginatedResult<CategoryDto>>> GetCategoriesWithTheirRecipesAsync(CategoryPaginationParams paginationParams);
        Task<CustomResponse> DeleteCategoryAsync(int id);
        Task<CustomResponse<CategoryDto>> AddCategory(AddCategoryDto categoryDto);
    }
}
