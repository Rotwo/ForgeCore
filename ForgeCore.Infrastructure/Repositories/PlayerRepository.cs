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

        public async Task CreateAsync(Player player)
        {
            _db.Players.Add(player);
            await _db.SaveChangesAsync();
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            return await _db.Players.FindAsync(id);
        }

        public async Task<List<Player>?> GetPlayersAsync()
        {
            return await _db.Players.ToListAsync();
        }

        public async Task<Player?> UpdateDisplayNameAsync(Guid id, string newDisplayName)
        {
            var player = await _db.Players.FindAsync(id);
            if (player == null)
                return null;
            player.DisplayName = newDisplayName;
            await _db.SaveChangesAsync();
            return player;
        }
    }
}
