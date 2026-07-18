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

        public void Add(Player player)
        {
            _db.Players.Add(player);
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

        public async void UpdateNickname(Guid id, string newName)
        {
            var player = await _db.Players.FirstOrDefaultAsync(p => p.Id == id);

            if (player is null)
                throw new InvalidOperationException($"Player with id {id} not found.");

            player.Rename(newName);
        }

        // Persistence is handled by UnitOfWork


    }
}
