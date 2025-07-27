using Common.Responses;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PaginatedResult<Category>> GetCategoriesWithRecipesAsync(CategoryPaginationParams paginationParams);
    }
}
