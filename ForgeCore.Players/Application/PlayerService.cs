using ForgeCore.Players.Contracts;
using ForgeCore.Players.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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
            var player = new Player()
            {
                CreatedAt = DateTime.UtcNow,
                DisplayName = "Guest",
                Id = Guid.NewGuid()
            };

            await _playerRepository.CreateAsync(player);

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

        public async Task<Player?> UpdateDisplayNameAsync(Guid id, string newName)
        {
            return await _playerRepository.UpdateDisplayNameAsync(id, newName);
        }
    }
}
