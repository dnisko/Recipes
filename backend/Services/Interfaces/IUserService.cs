using Common.Responses;
using DTOs.UserDto;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserDto registerUser);
        Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserDto loginUser);
        Task<CustomResponse<UserDto>> GetUserByIdAsync(int id);
        Task<CustomResponse<List<UserDto>>> GetAllUsersAsync();
        //Task<CustomResponse<AddUserDto>> CreateUserAsync(UserDto user);
        Task<CustomResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto updateUser, string username);
        Task<CustomResponse> DeleteUserAsync(int id);
    }
}
