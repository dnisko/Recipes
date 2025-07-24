namespace DTOs.UserDto
{
    public class UpdateUserResponseDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DomainModels.Enums.UserRole Role { get; set; }
    }
}
