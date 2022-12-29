using Data.Enums;

namespace Data
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public UserRoles Role { get; set; }
    }
}
