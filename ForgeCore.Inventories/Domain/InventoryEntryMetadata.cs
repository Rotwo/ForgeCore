using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Inventories.Domain
{
    public class InventoryEntryMetadata
    {
        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();
    }
}
