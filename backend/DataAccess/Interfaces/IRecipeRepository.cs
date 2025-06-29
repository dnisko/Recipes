using Common.Responses;
using DomainModels;
using DTOs;

namespace DataAccess.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<PaginatedResult<Recipe>> GetAllRecipesAsync(RecipePaginationParams paginationParams);
        Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId);
        Task<IEnumerable<Recipe>> SearchRecipes(string keyword);
        Task<Recipe> GetRecipeDetails(int recipeId);
        Task AddRecipeWithRelationsAsync(Recipe recipe);
        Task UpdateRecipeWithRelationsAsync(Recipe recipe);
    }
}
