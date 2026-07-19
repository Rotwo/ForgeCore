using ForgeCore.Players.Contracts;
using ForgeCore.Players.Domain;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Players.Application
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> CreateGuestAsync(Guid accountId)
        {
            var player = new Player(accountId: accountId, nickname: "Guest");

            _playerRepository.Add(player);
            await _unitOfWork.SaveChangesAsync();

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

        public async Task UpdateNicknameAsync(Guid id, string newName)
        {
            await _playerRepository.UpdateNickname(id, newName);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
