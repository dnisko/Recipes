using DomainModels.Enums;

namespace DTOs.UserDto
{
    public class ChangeUserRoleDto
    {
        //public int Id { get; set; }
        /// <summary>
        /// Role to assign to the user. Valid values: "User", "Admin", etc.
        /// </summary>
        public UserRole Role { get; set; }
    }
}
