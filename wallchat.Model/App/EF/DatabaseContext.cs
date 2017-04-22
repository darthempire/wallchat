using System.Data.Entity;
using wallchat.Model.App.Entity;

namespace wallchat.Model.App.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base ("LocalConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> News { get; set; }
        public DbSet<File> Files { get; set; }

        public void Commit()
        {
            SaveChanges( );
        }

        public void CommitAsync()
        {
            SaveChangesAsync( );
        }
    }
}