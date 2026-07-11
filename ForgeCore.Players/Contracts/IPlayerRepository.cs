using ForgeCore.Players.Domain;

namespace ForgeCore.Players.Contracts
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void UpdateNickname(Guid id, string newName);
        Task<List<Player>> GetPlayersAsync();
        Task<Player?> GetByIdAsync(Guid id);
        Task<Player?> GetByAccountIdAsync(Guid accountId);
        // Persist changes is handled by IUnitOfWork
    }
}
