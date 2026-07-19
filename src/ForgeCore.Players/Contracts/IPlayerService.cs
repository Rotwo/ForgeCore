using ForgeCore.Players.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Players.Contracts
{
    public interface IPlayerService
    {
        Task<Player> CreateGuestAsync(Guid accountId);
        Task<Player?> GetByIdAsync(Guid id);
        Task<Player?> GetByAccountIdAsync(Guid accountId);
        Task<List<Player>?> GetAllAsync();
        Task UpdateNicknameAsync(Guid id, string newName);
        Task<bool> IsOwnedByAccountAsync(Guid playerId, Guid accountId);
    }
}
