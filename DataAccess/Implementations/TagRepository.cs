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
            //return await  _context.Tags
            //    .OrderByDescending(t => _context.RecipeTags.Count(rt => rt.TagId == t.Id))
            //    .Take(top)
            //    .ToListAsync();
            return await _context.RecipeTags
                .GroupBy(rt => rt.Tag)
                .OrderByDescending(g => g.Count()) // Order by tag usage count
                .Take(top)
                .Select(g => g.Key)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByRecipe(int recipeId)
        {
            return await _context.RecipeTags
                .Where(rt => rt.RecipeId == recipeId)
                .Select(rt => rt.Tag)
                .ToListAsync();

        }
    }
}
