using ForgeCore.Inventories.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Inventories.Contracts
{
    public interface IInventoryRepository
    {
        void Add(Inventory inventory);
        Task RemoveById(Guid inventoryId);
        Task<Inventory?> GetByIdAsync(Guid inventoryId);
        Task<Inventory?> GetByOwnerIdAsync(Guid ownerId);
        // Persist changes is handled by IUnitOfWork
    }
}
