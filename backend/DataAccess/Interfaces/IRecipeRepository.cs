using DomainModels;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipesWithTags();
        Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId);
        Task<IEnumerable<Recipe>> SearchRecipes(string keyword);
        Task<Recipe> GetRecipeDetails(int recipeId);

        //Task<Recipe> GetRecipeWithIngredientsAsTask(int recipeId);
    }
}
