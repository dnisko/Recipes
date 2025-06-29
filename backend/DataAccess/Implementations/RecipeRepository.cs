using Common.Helpers;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private readonly RecipeDbContext _context;
        public RecipeRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Recipe>> GetAllRecipesAsync(RecipePaginationParams paginationParams)
        {
            var query = _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeTags).ThenInclude(rt => rt.Tag)
                .AsQueryable();

            return await PaginationHelper.ApplyPaginationAsync(query, paginationParams);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId)
        {
            return await _context.Recipes
                .Where(x => x.CategoryId == categoryId)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> SearchRecipes(string keyword)
        {
            return await _context.Recipes
                .Where(x => x.Name.ToLower().Contains(keyword.ToLower()) ||
                            x.Description.ToLower().Contains(keyword.ToLower()))
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeDetails(int recipeId)
        {
            return await _context.Recipes
                //.Where(r => r.Id == recipeId)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
        }

        public async Task AddRecipeWithRelationsAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipeWithRelationsAsync(Recipe recipe)
        {
            var existingRecipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.RecipeTags)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existingRecipe == null)
                throw new KeyNotFoundException($"Recipe with Id {recipe.Id} not found.");

            // Update simple properties
            _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);

            // Synchronize Ingredients (RecipeIngredients)
            existingRecipe.RecipeIngredients.Clear();
            foreach (var ri in recipe.RecipeIngredients)
            {
                existingRecipe.RecipeIngredients.Add(ri);
            }

            // Synchronize Tags (RecipeTags)
            existingRecipe.RecipeTags.Clear();
            foreach (var rt in recipe.RecipeTags)
            {
                existingRecipe.RecipeTags.Add(rt);
            }

            await _context.SaveChangesAsync();
        }
    }
}
