using Common.Responses;
using DomainModels.Enums;
using DTOs.Pagination;
using DTOs.UserDto;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserDto registerUser);
        Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserDto loginUser);
        Task<CustomResponse<UserResponseDto>> GetUserByIdAsync(int id);
        Task<CustomResponse<PaginatedResult<UserResponseDto>>> GetAllUsersAsync(UserPaginationParams paginationParams);
        //Task<CustomResponse<AddUserDto>> CreateUserAsync(UserDto user);
        Task<CustomResponse<UpdateUserResponseDto>> UpdateUserAsync(UpdateUserDto updateUser, string username);
        Task<CustomResponse> DeleteUserAsync(int id);
        Task<CustomResponse<UpdateUserResponseDto>> ChangeUserRoleAsync(int id, ChangeUserRoleDto dto);
    }
}
