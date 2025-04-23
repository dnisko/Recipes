using Common.Responses;
using DTOs.IngredientDto;

namespace Services.Interfaces
{
    public interface IIngredientService
    {
        Task<CustomResponse<List<IngredientDto>>> GetIngredientsByRecipe(int recipeId);
    }
}
