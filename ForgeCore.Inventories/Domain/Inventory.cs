using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Inventories.Domain
{
    public class Inventory
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }

        private readonly List<InventoryEntry> _entries = new();
        public IReadOnlyCollection<InventoryEntry> Entries => _entries;

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        private Inventory() { }

        public Inventory(Guid playerId)
        {
            Id = Guid.NewGuid();
            OwnerId = playerId;

            CreatedAt = DateTime.Now;
        }

        public void AddEntry(InventoryEntry entry)
        {
            _entries.Add(entry);
        }

        public void RemoveEntry(InventoryEntry entry)
        {
            _entries.Remove(entry);
        }

        public void ModifyEntry(InventoryEntry newEntry)
        {
            _entries.Find(e => e.Id == newEntry.Id)?.Update(newEntry);
        }

        public void ClearEntries()
        {
            _entries.Clear();
        }
    }
}
