using ForgeCore.Auth.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ForgeCore.Auth.Contracts.Requests;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("guest")]
        public async Task<IActionResult> LoginGuest()
        {
            var result = await _authService.LoginGuestAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            if (string.IsNullOrEmpty(request?.RefreshToken))
                return BadRequest("refreshToken is required");

            var result = await _authService.RefreshAsync(request.RefreshToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            if (request == null || request.SessionId == Guid.Empty)
                return BadRequest("sessionId is required");

            await _authService.LogoutAsync(request.SessionId);
            return NoContent();
        }
    }
}
