using DomainModels.Enums;

namespace DomainModels
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRole? Role { get; set; } = UserRole.User; // e.g., "Admin", "User"
    }
}
