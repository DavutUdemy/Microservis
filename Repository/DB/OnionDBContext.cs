using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository.DB
{   
    public partial class OnionDBContext : DbContext
    {
        public OnionDBContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Identity;Username=postgres;Password=514010191");
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
