namespace Data.Tables
{
    public class PasswordHash
    {
        public Guid PasswordHashId { get; set; }

        public string HashedPassword { get; set; }

        public byte[] Salt { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
