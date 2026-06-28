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

        public async Task<Player> CreateGuestAsync(Guid accountId)
        {
            var player = new Player(accountId: accountId, nickname: "Guest");

            await _playerRepository.AddAsync(player);

            return player;
        }

        public async Task<List<Player>?> GetAllAsync()
        {
            return await _playerRepository.GetPlayersAsync();
        }

        public async Task<Player?> GetByAccountIdAsync(Guid accountId)
        {
            return await _playerRepository.GetByAccountIdAsync(accountId);
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            return await _playerRepository.GetByIdAsync(id);
        }

        public async Task<Player?> UpdateNicknameAsync(Guid id, string newName)
        {
            return await _playerRepository.UpdateNicknameAsync(id, newName);
        }
    }
}
