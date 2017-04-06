using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wallchat.Model.App.Entity;

namespace wallchat.Model.App.EF
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DatabaseContext()
            : base("LocalConnection")
        {

        }

        public void Commit ()
        {
            SaveChanges ( );
        }

        public void CommitAsync()
        {
            SaveChangesAsync ( );
        }
    }
}
