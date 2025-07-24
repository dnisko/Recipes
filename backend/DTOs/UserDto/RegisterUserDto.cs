using System.ComponentModel.DataAnnotations;

namespace DTOs.UserDto
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //public string? Role { get; set; } // Default role is "User"
    }
}
