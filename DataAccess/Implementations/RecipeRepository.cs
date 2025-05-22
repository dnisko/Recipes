using DataAccess.Interfaces;
using DomainModels;
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

        public async Task<IEnumerable<Recipe>> GetRecipesWithTags()
        {
            return await _context.Recipes
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId)
        {
            return await _context.Recipes
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> SearchRecipes(string keyword)
        {
            return await _context.Recipes
                .Where(x => x.Name.ToLower().Contains(keyword.ToLower()) ||
                            x.Description.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeDetails(int recipeId)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
        }
    }
}
