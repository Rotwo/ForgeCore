using ForgeCore.Auth.Domain;
using ForgeCore.Economy.Domain;
using ForgeCore.Inventories.Domain;
using ForgeCore.Players.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Persistence
{
    public class ForgeCoreDbContext : DbContext
    {
        public ForgeCoreDbContext(DbContextOptions<ForgeCoreDbContext> options) : base(options)
        { }

        // - DbSet's for each entity in the domain model -

        // Authentication Related DbSet's
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AuthProvider> AuthProviders => Set<AuthProvider>();
        public DbSet<Session> Sessions => Set<Session>();

        // Player Related DbSet's
        public DbSet<Player> Players => Set<Player>();

        // Inventory Related DbSet's
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<InventoryEntry> InventoryEntries => Set<InventoryEntry>();

        // Economy Related DbSet's
        public DbSet<Wallet> Wallets => Set<Wallet>();

        // Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForgeCoreDbContext).Assembly);
        }
    }
}
