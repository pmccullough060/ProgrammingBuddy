using Data.Enums;

namespace Data.Tables
{
    public class UserProject
    {
        public Guid UserProjectId { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Code { get; set; }

        public Languages Language { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
