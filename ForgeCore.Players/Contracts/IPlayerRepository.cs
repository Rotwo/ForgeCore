using ForgeCore.Players.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Players.Contracts
{
    public interface IPlayerRepository
    {
        Task CreateAsync(Player player);
        Task<List<Player>?> GetPlayersAsync();
        Task<Player?> GetByIdAsync(Guid id);
        Task<Player?> UpdateDisplayNameAsync(Guid id, string newDisplayName);
    }
}
