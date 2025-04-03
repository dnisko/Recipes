using DomainModels;

namespace DataAccess.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetPopularTags(int top = 10);
        Task<IEnumerable<Tag>> GetTagsByRecipe(int recipeId);
    }
}
