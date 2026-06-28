using ForgeCore.Players.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Players.Contracts
{
    public interface IPlayerService
    {
        Task<Player> CreateGuestAsync();
        Task<Player?> GetByIdAsync(Guid id);
        Task<List<Player>?> GetAllAsync();
        // Task<Player?> UpdateDisplayNameAsync(Guid id, string newName);
    }
}
