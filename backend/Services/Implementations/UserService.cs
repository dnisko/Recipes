using AutoMapper;
using Common.Exceptions.UserException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
using DTOs.RecipeDto;
using DTOs.UserDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IUserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            ILogger<IUserService> logger,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserDto registerUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registerUser.Username) || string.IsNullOrWhiteSpace(registerUser.Password))
                {
                    //_logger.LogInfo("Username and password must be provided!");
                    _logger.LogError("Username and password must be provided!");
                    return CustomResponse<RegisterUserResponseDto>.Fail("Username and password must be provided!");
                    //return CustomResponseFactory.FromList(registerUser, "Username and password must be provided!");
                }
                //var mappedUser = _mapper.Map<User>(registerUser);
                var hashedPassword = HashPassword(registerUser.Password);
                var user = new User
                {
                    Username = registerUser.Username,
                    PasswordHash = hashedPassword,
                    Email = registerUser.Email,
                    Role = DomainModels.Enums.UserRole.User
                };
                await _userRepository.Register(user);

                //_logger.LogInfo($"User with username \"{registerUser.Username}\" was added.");
                var response = _mapper.Map<RegisterUserResponseDto>(user);
                _logger.LogInformation($"User with username \"{registerUser.Username}\" was added.");
                return CustomResponse<RegisterUserResponseDto>.Success(response, $"User with username \"{registerUser.Username}\" was added.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log specific EF-related exception
                _logger.LogError(dbEx, "Database update error occurred.");
                throw;
            }
            catch (UserDataException ex)
            {
                _logger.LogError(ex, "Error while registering user!");
                throw new UserDataException(ex.Message);
            }
        }

        public async Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserDto loginUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginUser.Username) || string.IsNullOrWhiteSpace(loginUser.Password))
                {
                    _logger.LogInformation("Username and password must be provided!");
                    return CustomResponse<LoginUserResponseDto>.Fail("Username and password must be provided!");
                }
                var user = await _userRepository.GetSingleUserByUsernameAsync(loginUser.Username);
                if (user == null)
                {
                    _logger.LogError($"User with username \"{loginUser.Username}\" not found.");
                    return CustomResponse<LoginUserResponseDto>.Fail($"User with username \"{loginUser.Username}\" not found.");
                }
                var hashedPassword = HashPassword(loginUser.Password);
                if (user.PasswordHash != hashedPassword)
                {
                    _logger.LogError($"Invalid password for user \"{loginUser.Username}\".");
                    return CustomResponse<LoginUserResponseDto>.Fail($"Invalid password for user \"{loginUser.Username}\".");
                }
                //var checkPassword = await _userRepository.CheckPasswordAsync(hashedPassword);
                //if (checkPassword == null)
                //{
                //    _logger.LogError($"Invalid password for user \"{loginUser.Username}\".");
                //    return CustomResponse<LoginUserResponseDto>.Fail($"Invalid password for user \"{loginUser.Username}\".");
                //}

                var token = await _tokenService.GenerateTokenAsync(user);

                return CustomResponse<LoginUserResponseDto>.Success(new LoginUserResponseDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ValidTo = token.ValidTo
                    });
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the user: {ex.Message}");
            }
        }

        public async Task<CustomResponse<UserResponseDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found.");
                    return CustomResponse<UserResponseDto>.Fail($"User with id {id} not found.");
                }
                var userDto = _mapper.Map<UserResponseDto>(user);
                return CustomResponse<UserResponseDto>.Success(userDto);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the users: {ex.Message}");
            }
        }

        public async Task<CustomResponse<PaginatedResult<UserResponseDto>>> GetAllUsersAsync(UserPaginationParams paginationParams)
        {
            try
            {
                var paged = await _userRepository.GetPagedAsync(paginationParams);
                if (!paged.Items.Any())
                    return CustomResponse<PaginatedResult<UserResponseDto>>.Fail("No recipes found.");
                //var response = CustomResponseFactory.FromList(users, "No users found.");

                var mapped = _mapper.Map<List<UserResponseDto>>(paged.Items);
                var result = new PaginatedResult<UserResponseDto>(mapped, paged.TotalRecords, paginationParams.PageNumber, paginationParams.PageSize);
                return CustomResponse<PaginatedResult<UserResponseDto>>.Success(result);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the users: {ex.Message}");
            }
        }

        public async Task<CustomResponse<UpdateUserResponseDto>> UpdateUserAsync(UpdateUserDto updateUser, string username)
        {
            try
            {
                var user = await _userRepository.GetSingleUserByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogInformation("User not found!");
                    return CustomResponse<UpdateUserResponseDto>.Fail($"User with username: \"{username}\" not found!");
                }

                if (!string.IsNullOrEmpty(updateUser.Password))
                {
                    updateUser.Password = HashPassword(updateUser.Password);
                }

                _mapper.Map(updateUser, user);
                await _userRepository.UpdateAsync(user);
                var updatedUserDto = _mapper.Map<UpdateUserResponseDto>(user);
                return CustomResponse<UpdateUserResponseDto>.Success(updatedUserDto);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the user: {ex.Message}");
            }
        }

        public async Task<CustomResponse> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found.");
                    return CustomResponse<UpdateUserDto>.Fail($"User with id {id} not found.");
                }
                await _userRepository.DeleteAsync(user);
                _logger.LogInformation($"User with id {id} deleted.");
                return CustomResponse.Success($"User with id {id} deleted.");
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the user: {ex.Message}");
            }
        }

        public async Task<CustomResponse<UpdateUserResponseDto>> MakeAdminAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("Invalid user ID provided.");
                    return CustomResponse<UpdateUserResponseDto>.Fail("Invalid user ID provided.");
                }

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found.");
                    return CustomResponse<UpdateUserResponseDto>.Fail($"User with id {id} not found.");
                }
                if (user.Role == DomainModels.Enums.UserRole.Admin)
                {
                    _logger.LogInformation($"User with id {id} is already an admin.");
                    return CustomResponse<UpdateUserResponseDto>.Fail($"User with id {id} is already an admin.");
                }
                user.Role = DomainModels.Enums.UserRole.Admin;
                await _userRepository.UpdateAsync(user);
                _logger.LogInformation($"User with id {id} is now an admin.");
                var userDto = _mapper.Map<UpdateUserResponseDto>(user);
                return CustomResponse<UpdateUserResponseDto>.Success(userDto, $"User with id {id} is now an admin.");
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while making user an admin: {ex.Message}");
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
