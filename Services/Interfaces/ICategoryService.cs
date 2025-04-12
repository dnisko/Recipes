using Common.Responses;
using DTOs.CategoryDto;
using System.Threading.Tasks;
namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CustomResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<CustomResponse<List<CategoryDto>>> GetCategoriesWithTheirRecipesAsync();
        Task<CustomResponse> DeleteCategoryAsync(int id);
    }
}
