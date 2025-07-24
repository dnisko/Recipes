namespace DTOs.UserDto
{
    public class RegisterUserResponseDto
    {
        //public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; } // e.g., "Admin", "User"
    }
}
