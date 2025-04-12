using DTOs.UserDto;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}
