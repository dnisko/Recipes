using DomainModels;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithRecipesAsync();
    }
}
