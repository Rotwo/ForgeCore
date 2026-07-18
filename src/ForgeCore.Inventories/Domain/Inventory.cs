using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForgeCore.Inventories.Domain
{
    public class Inventory
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }

        private readonly List<InventoryEntry> _entries = new();
        public IReadOnlyCollection<InventoryEntry> Entries => _entries;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public byte[] RowVersion { get; set; }

        private Inventory() { }

        public Inventory(Guid ownerId)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;

            CreatedAt = DateTime.UtcNow;
        }

        public void AddEntry(InventoryEntry entry)
        {
            var existing = _entries.FirstOrDefault(e =>
                e.ItemKey == entry.ItemKey &&
                e.IsStackable);

            if (existing is not null)
            {
                existing.Increase(entry.Quantity);
                return;
            }

            _entries.Add(entry);
        }

        public void RemoveEntry(Guid entryId)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == entryId);

            if (entry is null)
                throw new InvalidOperationException($"Entry {entryId} not found.");

            _entries.Remove(entry);
        }

        public void ModifyEntry(Guid oldEntryId, InventoryEntry newEntry)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == oldEntryId);

            if (entry is null)
                throw new InvalidOperationException($"Entry {oldEntryId} not found.");

            entry.Update(newEntry);
        }

        public void ClearEntries()
        {
            _entries.Clear();
        }
    }
}
