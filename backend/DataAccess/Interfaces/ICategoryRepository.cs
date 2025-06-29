using Common.Responses;
using DomainModels;
using DTOs;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PaginatedResult<Category>> GetCategoriesWithRecipesAsync(PaginationParams paginationParams);
    }
}
