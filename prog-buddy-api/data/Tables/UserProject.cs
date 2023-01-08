namespace Data.Tables
{
    public class UserProject
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public string Code { get; set; }
    }
}
