using Common.Helpers;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
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

        public async Task<PaginatedResult<Category>> GetCategoriesWithRecipesAsync(PaginationParams paginationParams)
        {
            var categories = _context.Categories
                .Include(c => c.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(c => c.Recipes)
                    .ThenInclude(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .AsQueryable();
            return await PaginationHelper.ApplyPaginationAsync(categories, paginationParams);
        }
    }
}
