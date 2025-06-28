using Common.Responses;
using DomainModels;
using DTOs;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<PaginatedResult<Recipe>> GetAllRecipesDetails1(int pageNumber, int pageSize);
        Task<PaginatedResult<Recipe>> GetAllRecipesAsync(RecipePaginationParams paginationParams);
        Task<IEnumerable<Recipe>> GetRecipesWithTags();
        Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId);
        Task<IEnumerable<Recipe>> SearchRecipes(string keyword);
        Task<Recipe> GetRecipeDetails(int recipeId);

        //Task<Recipe> GetRecipeWithIngredientsAsTask(int recipeId);
        Task AddRecipeWithRelationsAsync(Recipe recipe);
        Task UpdateRecipeWithRelationsAsync(Recipe recipe);
    }
}
