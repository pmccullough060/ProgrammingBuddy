namespace Data.Tables
{
    public class UserProject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public string Code { get; set; }
    }
}
