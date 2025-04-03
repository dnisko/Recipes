using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly RecipeDbContext _context;
        public CategoryRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithRecipes()
        {
            return await _context.Categories
                .Where(c => c.Recipes.Any()) //for categories with at least one recipe
                .Include(x => x.Recipes)
                .ToListAsync();
        }
    }
}
