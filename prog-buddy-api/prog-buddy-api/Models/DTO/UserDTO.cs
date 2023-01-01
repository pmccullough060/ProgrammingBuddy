using Data.Enums;

namespace prog_buddy_api.Models.DTO
{
    public record UserDTO
    {
        public string UserName { get; init; }

        public UserRoles Role { get; init; }
    }
}
