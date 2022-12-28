using prog_buddy_api.Enums;

namespace prog_buddy_api.Models.DTO
{
    public record User
    {
        public string UserName { get; init; }

        public UserRoles Role { get; init; }
    }
}
