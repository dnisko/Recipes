using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<IEnumerable<Image>> GetImagesByRecipe(int recipeId);
    }
}
