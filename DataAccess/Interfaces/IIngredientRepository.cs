using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsByRecipe(int recipeId);
    }
}
