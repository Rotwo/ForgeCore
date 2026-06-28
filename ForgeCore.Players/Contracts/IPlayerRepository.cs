using ForgeCore.Players.Domain;

namespace ForgeCore.Players.Contracts
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        Task<List<Player>?> GetPlayersAsync();
        Task<Player?> GetByIdAsync(Guid id);
    }
}
