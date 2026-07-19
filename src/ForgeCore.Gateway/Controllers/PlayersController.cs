using ForgeCore.Players.Contracts;
using ForgeCore.Players.Contracts.Requests;
using ForgeCore.Shared.Contracts;
using ForgeCore.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ICurrentUserService _currentUser;

        public PlayersController(IPlayerService playerService, ICurrentUserService currentUser)
        {
            _playerService = playerService;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGuest()
        {
            try
            {
                var player = await _playerService.CreateGuestAsync(_currentUser.AccountId);
                return Ok(player);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the guest player.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("id is required");

            try
            {
                var player = await _playerService.GetByIdAsync(id);
                if (player == null)
                    return NotFound();
                return Ok(player);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the player.");
            }
        }

        [HttpGet("account/{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetByAccountId(Guid accountId)
        {
            if (accountId == Guid.Empty)
                return BadRequest("accountId is required");

            try
            {
                var player = await _playerService.GetByAccountIdAsync(accountId);
                if (player == null)
                    return NotFound();
                return Ok(player);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the player.");
            }
        }

        [HttpPatch("{id}/nickname")]
        [Authorize]
        public async Task<IActionResult> UpdateNickname(Guid id, [FromBody] UpdateNicknameRequest request)
        {
            if (id == Guid.Empty)
                return BadRequest("id is required");
            if (string.IsNullOrWhiteSpace(request.NewName) || request == null)
                return BadRequest("newName is required");

            try
            {
                await _playerService.UpdateNicknameAsync(id, request.NewName);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the player's nickname.");
            }
        }
    }
}
