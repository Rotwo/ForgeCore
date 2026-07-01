using ForgeCore.Inventories.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Inventories.Contracts
{
    public interface IInventoryRepository
    {
        Task AddAsync(Inventory inventory);
        Task DeleteAsync(Guid inventoryId);
        Task<Inventory?> GetByIdAsync(Guid inventoryId);
        Task<Inventory?> GetByOwnerIdAsync(Guid ownerId);
        Task SaveChangesAsync();
    }
}
