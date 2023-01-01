using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<PasswordHash> PasswordHashes { get; set; }
    }
}