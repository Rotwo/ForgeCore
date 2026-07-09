using ForgeCore.Inventories.Contracts;
using ForgeCore.Inventories.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using ForgeCore.Inventories.Contracts.Requests;
using ForgeCore.Inventories.Contracts.Responses;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateInventoryRequest request)
        {
            if (request == null || request.OwnerId == Guid.Empty)
                return BadRequest("ownerId is required");

            try
            {
                var inventory = await _inventoryService.CreateInventoryAsync(request.OwnerId);
                return Ok(ToResponse(inventory));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("owner/{ownerId}")]
        [Authorize]
        public async Task<IActionResult> GetByOwner(Guid ownerId)
        {
            if (ownerId == Guid.Empty)
                return BadRequest("ownerId is required");

            try
            {
                var inventory = await _inventoryService.GetInventoryAsync(ownerId);
                if (inventory == null)
                    return NotFound();
                return Ok(ToResponse(inventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{inventoryId}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid inventoryId)
        {
            if (inventoryId == Guid.Empty)
                return BadRequest("inventoryId is required");

            await _inventoryService.DeleteInventoryAsync(inventoryId);
            return NoContent();
        }

        [HttpPost("{inventoryId}/entries")]
        [Authorize]
        public async Task<IActionResult> AddEntry(Guid inventoryId, [FromBody] AddEntryRequest request)
        {
            if (inventoryId == Guid.Empty)
                return BadRequest("inventoryId is required");
            if (request == null || string.IsNullOrWhiteSpace(request.ItemKey))
                return BadRequest("itemKey is required");

            try
            {
                var entry = new InventoryEntry(request.ItemKey, request.Quantity, request.SlotIndex, request.IsStackable);
                ApplyMetadata(entry, request.Metadata);

                await _inventoryService.AddEntryAsync(inventoryId, entry);
                return Ok(ToResponse(entry));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Surface the exception message for debugging (can be changed to logging in production)
                return StatusCode(500, $"An error occurred while adding the entry: {ex.Message}");
            }
        }

        [HttpDelete("{inventoryId}/entries/{entryId}")]
        [Authorize]
        public async Task<IActionResult> RemoveEntry(Guid inventoryId, Guid entryId)
        {
            if (inventoryId == Guid.Empty || entryId == Guid.Empty)
                return BadRequest("inventoryId and entryId are required");

            try
            {
                await _inventoryService.RemoveEntryAsync(inventoryId, entryId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{inventoryId}/entries/{entryId}")]
        [Authorize]
        public async Task<IActionResult> UpdateEntry(Guid inventoryId, Guid entryId, [FromBody] UpdateEntryRequest request)
        {
            if (inventoryId == Guid.Empty || entryId == Guid.Empty)
                return BadRequest("inventoryId and entryId are required");
            if (request == null || string.IsNullOrWhiteSpace(request.ItemKey))
                return BadRequest("itemKey is required");

            try
            {
                var entry = new InventoryEntry(entryId, request.ItemKey, request.Quantity, request.SlotIndex, request.IsStackable);
                ApplyMetadata(entry, request.Metadata);

                await _inventoryService.UpdateEntryAsync(inventoryId, entry);
                return Ok(ToResponse(entry));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        private static void ApplyMetadata(InventoryEntry entry, JObject? metadata)
        {
            if (metadata is null)
                return;

            foreach (var property in metadata.Properties())
            {
                var json = property.Value.ToString(Formatting.None);
                var value = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(json);

                entry.SetMetadata(property.Name, value);
            }
        }

        private static InventoryEntryResponse ToResponse(InventoryEntry entry)
        {
            return new InventoryEntryResponse
            {
                Id = entry.Id,
                InventoryId = entry.InventoryId,
                ItemKey = entry.ItemKey,
                Quantity = entry.Quantity,
                SlotIndex = entry.SlotIndex,
                IsStackable = entry.IsStackable,
                Metadata = entry.Metadata.Values.ToDictionary(
                    pair => pair.Key,
                    pair => ToNewtonsoftValue(pair.Value))
            };
        }

        private static InventoryResponse ToResponse(Inventory inventory)
        {
            return new InventoryResponse
            {
                Id = inventory.Id,
                OwnerId = inventory.OwnerId,
                CreatedAt = inventory.CreatedAt,
                Entries = inventory.Entries.Select(ToResponse).ToList()
            };
        }

        private static object? ToNewtonsoftValue(JsonElement value)
        {
            return value.ValueKind switch
            {
                JsonValueKind.Undefined or JsonValueKind.Null => null,
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Number when value.TryGetInt64(out var longValue) => longValue,
                JsonValueKind.Number when value.TryGetDouble(out var doubleValue) => doubleValue,
                JsonValueKind.String => value.GetString(),
                _ => JsonConvert.DeserializeObject<object?>(value.GetRawText())
            };
        }
    }
}
