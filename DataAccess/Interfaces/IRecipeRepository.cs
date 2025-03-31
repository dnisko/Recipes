using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> getRecipeWithIngredientsAsTask(int recipeId);
    }
}
