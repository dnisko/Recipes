using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly RecipeDbContext _context;
        public ImageRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetImagesByRecipe(int recipeId)
        {
            return null; //await _context.Images.Where(x => x.RecipeId == recipeId).ToListAsync();
        }
    }
}
