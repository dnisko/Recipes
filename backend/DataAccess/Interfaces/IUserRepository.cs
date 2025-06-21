using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string username, string hashPassword);
        Task<IEnumerable<User>> GetByUsernameAsync(string username);
        Task<User> GetSingleUserByUsernameAsync(string username);
        Task<User> CheckPasswordAsync(string hashPassword);
        Task<User> Register(User registerUser);
    }
}
