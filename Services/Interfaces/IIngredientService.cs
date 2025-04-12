using DTOs.IngredientDto;

namespace Services.Interfaces
{
    public interface IIngredientService
    {
        Task<List<IngredientDto>> GetAllIngredientsByRecipeIdAsync(int recipeId);
    }
}
