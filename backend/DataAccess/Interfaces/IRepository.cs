using Common.Responses;
using DomainModels;
using DTOs;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //Task<PaginatedResult<T>> GetPagedAsync(
        //    IQueryable<T> query,
        //    int pageNumber,
        //    int pageSize);

        Task<PaginatedResult<T>> GetPagedAsync(PaginationParams paginationParams);
    }
}
