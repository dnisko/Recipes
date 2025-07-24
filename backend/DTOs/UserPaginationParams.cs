namespace DTOs
{
    public class UserPaginationParams : PaginationParams
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DomainModels.Enums.UserRole? Role { get; set; }
    }
}
