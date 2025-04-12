using DTOs.RecipeDto;

namespace Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
        Task<RecipeDto> GetRecipeByIdAsync(int id);
        Task AddRecipeAsync(RecipeDto recipe);
        Task UpdateRecipeAsync(RecipeDto recipe);
        Task DeleteRecipeAsync(int id);
    }
}
