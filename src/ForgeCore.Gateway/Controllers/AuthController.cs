using ForgeCore.Auth.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ForgeCore.Auth.Contracts.Requests;
using System.Text.Json;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUser;

        public AuthController(IAuthService authService, ICurrentUserService currentUser)
        {
            _authService = authService;
            _currentUser = currentUser;
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

            if (request.SessionId != _currentUser.SessionId)
                return Forbid();


            await _authService.LogoutAsync(request.SessionId);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("email")]
        public async Task<IActionResult> LoginEmail([FromBody] EmailPasswordRequest request)
        {
            try
            {
                var result = await _authService.RegisterEmailAsync(request.Email, request.Password, request.DisplayName);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("email/login")]
        public async Task<IActionResult> LoginWithEmail([FromBody] EmailPasswordRequest request)
        {
            try
            {
                var credentials = JsonSerializer.Serialize(new
                {
                    Email = request.Email,
                    Password = request.Password
                });

                var result = await _authService.LoginAsync(Auth.Domain.AuthProviderType.EmailPassword, credentials);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
