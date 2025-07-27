using DomainModels.Enums;

namespace DTOs.Pagination
{
    public class UserPaginationParams : BasePaginationParams
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
    }
}
