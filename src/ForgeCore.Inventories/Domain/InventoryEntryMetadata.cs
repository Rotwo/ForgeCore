using System.Text.Json;

namespace ForgeCore.Inventories.Domain
{
    public sealed class InventoryEntryMetadata
    {
        private readonly Dictionary<string, JsonElement> _values = new(StringComparer.Ordinal);

        public IReadOnlyDictionary<string, JsonElement> Values => _values;

        public JsonElement this[string key]
        {
            get => _values[key];
            set => _values[key] = value.Clone();
        }

        public static InventoryEntryMetadata From(IDictionary<string, JsonElement>? values)
        {
            var metadata = new InventoryEntryMetadata();

            if (values is null)
                return metadata;

            foreach (var (key, value) in values)
                metadata[key] = value;

            return metadata;
        }

        public void Set(string key, object? value)
        {
            _values[key] = JsonSerializer.SerializeToElement(value);
        }

        public bool TryGetValue(string key, out JsonElement value)
        {
            return _values.TryGetValue(key, out value);
        }

        public InventoryEntryMetadata Clone()
        {
            return From(_values);
        }

        public Dictionary<string, JsonElement> ToDictionary()
        {
            return _values.ToDictionary(pair => pair.Key, pair => pair.Value.Clone(), StringComparer.Ordinal);
        }
    }
}
