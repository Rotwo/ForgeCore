using ForgeCore.Players.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Persistence
{
    public class ForgeCoreDbContext : DbContext
    {
        public ForgeCoreDbContext(DbContextOptions<ForgeCoreDbContext> options) : base(options)
        {}

        // DbSet's for each entity in the domain model
        public DbSet<Player> Players => Set<Player>();

        // Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForgeCoreDbContext).Assembly);
        }
    }
}
