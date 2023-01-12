using Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Tables
{
    public class User
    {
        public Guid UserId { get; set; }

        public string? Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserRoles Role { get; set; }

        public PasswordHash PasswordHash { get; set; }

        public List<UserProject> Projects { get; set; }
    }
}
