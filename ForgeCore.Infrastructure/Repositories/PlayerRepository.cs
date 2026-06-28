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

        public Task<Player?> GetByAccountIdAsync(Guid accountId)
        {
            return _db.Players.Where(p => p.AccountId == accountId).FirstOrDefaultAsync();
        }

        public Task<Player?> GetByIdAsync(Guid id)
        {
            return _db.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Player>> GetPlayersAsync()
        {
            return _db.Players.ToListAsync();
        }

        public async Task<Player?> UpdateNicknameAsync(Guid id, string newName)
        {
            var player = await _db.Players.FirstOrDefaultAsync(p => p.Id == id);

            if (player is null)
                return null;

            player.Rename(newName);

            await _db.SaveChangesAsync();

            return player;
        }
    }
}
