namespace Data.Tables
{
    public class PasswordHash
    {
        public Guid Id { get; set; }

        public string HashedPassword { get; set; }

        public byte[] Salt { get; set; }
    }
}
