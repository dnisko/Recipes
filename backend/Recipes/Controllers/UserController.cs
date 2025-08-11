using Common.Exceptions.ServerException;
using Common.Exceptions.UserException;
using DomainModels.Enums;
using DTOs.Pagination;
using DTOs.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            try
            {
                var response = await _userService.RegisterUserAsync(registerUser);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            try
            {
                var response = await _userService.LoginUserAsync(loginUser);
                if (!response.IsSuccessful)
                {
                    return Unauthorized(response);
                }

                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserByIdAsync(id);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserPaginationParams paginationParams)
        {
            try
            {
                if (string.IsNullOrEmpty(paginationParams.SortBy))
                    paginationParams.SortBy = "Username"; // or "Id"

                if (string.IsNullOrEmpty(paginationParams.SortDirection))
                    paginationParams.SortDirection = "asc";

                var response = await _userService.GetAllUsersAsync(paginationParams);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateUser/{username}")]
        public async Task<IActionResult> UpdateUser(string username, [FromBody] UpdateUserDto updateUser)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(updateUser, username);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _userService.DeleteUserAsync(id);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("changeRole/{id}")]
        [Authorize]//(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(int id, [FromBody] ChangeUserRoleDto dto)
        {
            try
            {
                var response = await _userService.ChangeUserRoleAsync(id, dto);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
