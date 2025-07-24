using Common.Responses;
using DomainModels;
using DTOs;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<PaginatedResult<User>> GetAllUsersAsync(UserPaginationParams paginationParams);
        Task<User> LoginAsync(string username, string hashPassword);
        Task<IEnumerable<User>> GetByUsernameAsync(string username);
        Task<User> GetSingleUserByUsernameAsync(string username);
        Task<User> CheckPasswordAsync(string hashPassword);
        Task<User> Register(User registerUser);
    }
}
