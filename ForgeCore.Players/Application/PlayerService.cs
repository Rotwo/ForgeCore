using ForgeCore.Players.Contracts;
using ForgeCore.Players.Domain;

namespace ForgeCore.Players.Application
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> CreateGuestAsync()
        {
            var player = new Player(accountId: Guid.NewGuid(), nickname: "Guest");

            await _playerRepository.AddAsync(player);

            return player;
        }

        public async Task<List<Player>?> GetAllAsync()
        {
            return await _playerRepository.GetPlayersAsync();
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            return await _playerRepository.GetByIdAsync(id);
        }
    }
}
