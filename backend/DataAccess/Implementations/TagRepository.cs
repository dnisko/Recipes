using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly RecipeDbContext _context;
        public TagRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetPopularTags(int top = 10)
        {
            return await _context.Tags
                .OrderByDescending(t => t.Recipes.Count)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByRecipe(int recipeId)
        {
            return await _context.Tags
                .Include(t => t.Recipes)
                .Where(t => t.Recipes.Any(r => r.Id == recipeId))
                .ToListAsync();

        }
    }
}
