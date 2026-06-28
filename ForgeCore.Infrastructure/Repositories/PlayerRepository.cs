using ForgeCore.Infrastructure.Persistence;
using ForgeCore.Players.Contracts;
using ForgeCore.Players.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ForgeCoreDbContext _db;

        public PlayerRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public Task AddAsync(Player player)
        {
            _db.Players.Add(player);
            return _db.SaveChangesAsync();
        }

        public Task<Player?> GetByIdAsync(Guid id)
        {
            return _db.Players.FindAsync(id).AsTask();
        }

        public Task<List<Player>?> GetPlayersAsync()
        {
            return _db.Players.ToListAsync();
        }
    }
}
