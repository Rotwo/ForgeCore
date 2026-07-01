using ForgeCore.Inventories.Contracts;
using ForgeCore.Inventories.Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public class CreateInventoryRequest
        {
            public Guid OwnerId { get; set; }
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
                return Ok(inventory);
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
                return Ok(inventory);
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

        public class AddEntryRequest
        {
            public string ItemKey { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public int? SlotIndex { get; set; }
            public bool IsStackable { get; set; }
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

                await _inventoryService.AddEntryAsync(inventoryId, entry);
                return Ok(entry);
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

        public class UpdateEntryRequest
        {
            public Guid Id { get; set; }
            public string ItemKey { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public int? SlotIndex { get; set; }
            public bool IsStackable { get; set; }
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

                await _inventoryService.UpdateEntryAsync(inventoryId, entry);
                return Ok(entry);
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
    }
}
