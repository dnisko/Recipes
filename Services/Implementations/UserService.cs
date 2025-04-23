using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Common.Exceptions.UserException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.UserDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

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
                    return new CustomResponse<RegisterUserResponseDto>("Username and password must be provided!");
                }
                //var mappedUser = _mapper.Map<User>(registerUser);
                var hashedPassword = HashPassword(registerUser.Password);
                var user = new User { Username = registerUser.Username, PasswordHash = hashedPassword};
                await _userRepository.Register(user);

                //_logger.LogInfo($"User with username \"{registerUser.Username}\" was added.");
                _logger.LogInformation($"User with username \"{registerUser.Username}\" was added.");
                return new CustomResponse<RegisterUserResponseDto>($"User with username \"{registerUser.Username}\" was added.");
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
                    return new CustomResponse<LoginUserResponseDto>("Username and password must be provided!");
                }
                var user = await _userRepository.GetSingleUserByUsernameAsync(loginUser.Username);
                if (user == null)
                {
                    _logger.LogError($"User with username \"{loginUser.Username}\" not found.");
                    return new CustomResponse<LoginUserResponseDto>($"User with username \"{loginUser.Username}\" not found.");
                }
                var hashedPassword = HashPassword(loginUser.Password);
                var checkPassword = await _userRepository.CheckPasswordAsync(hashedPassword);
                if (checkPassword == null)
                {
                    _logger.LogError($"Invalid password for user \"{loginUser.Username}\".");
                    return new CustomResponse<LoginUserResponseDto>($"Invalid password for user \"{loginUser.Username}\".");
                }

                var token = await _tokenService.GenerateTokenAsync(user);

                return new CustomResponse<LoginUserResponseDto>(new LoginUserResponseDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ValidTo = token.ValidTo
                    });
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<UserDto>>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found.");
                    return new CustomResponse<List<UserDto>>($"User with id {id} not found.");
                }
                var userDto = _mapper.Map<List<UserDto>>(user);
                return new CustomResponse<List<UserDto>>(userDto);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users == null || !users.Any())
                {
                    _logger.LogError("No users found.");
                    return new CustomResponse<UpdateUserDto>($"No users found.");
                }
                var usersDto = _mapper.Map<List<UserDto>>(users);
                return new CustomResponse<List<UserDto>>(usersDto);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        //public Task<CustomResponse<AddUserDto>> CreateUserAsync(UserDto user)
        //{
        //    try
        //    {

        //    }
        //    catch (UserDataException ex)
        //    {
        //        throw new UserDataException($"Error while getting the categories: {ex.Message}");
        //    }
        //}

        public async Task<CustomResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto updateUser, string username)
        {
            try
            {
                var user = await _userRepository.GetSingleUserByUsernameAsync(username);
                //var user = usernames.FirstOrDefault();
                //var user1 = await _userRepository.GetByIdAsync(user.Id);
                if (user == null)
                {
                    _logger.LogInformation("User not found!");
                    return new CustomResponse<UpdateUserDto>($"User not found!");
                    //throw new UserNotFoundException("User not found!");
                }

                if (!string.IsNullOrEmpty(updateUser.Password))
                {
                    updateUser.Password = HashPassword(updateUser.Password);
                }

                _mapper.Map(updateUser, user);
                await _userRepository.UpdateAsync(user);

                return new CustomResponse<UpdateUserDto>(updateUser);
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the categories: {ex.Message}");
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
                    return new CustomResponse<UpdateUserDto>($"User with id {id} not found.");
                }
                await _userRepository.DeleteAsync(user);
                _logger.LogInformation($"User with id {id} deleted.");
                return new CustomResponse<UpdateUserDto>($"User with id {id} deleted.");
            }
            catch (UserDataException ex)
            {
                throw new UserDataException($"Error while getting the categories: {ex.Message}");
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
