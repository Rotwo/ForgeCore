using ForgeCore.Players.Contracts;
using ForgeCore.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers() { 
            var players = await _playerService.GetAllAsync();
            
            if(players == null || players.Count == 0)
                return NotFound();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(Guid id) 
        {
            var player = await _playerService.GetByIdAsync(id);

            if (player == null)
                return NotFound();

            return Ok(player);
        }

        [HttpPatch("{id}/display-name")]
        public async Task<IActionResult> UpdateDisplayName(Guid id, [FromBody] UpdateDisplayNameRequest request)
        {
            try
            {
                var player = await _playerService.UpdateDisplayNameAsync(id, request.NewName);
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the display name.");
            }
        }
    }
}
