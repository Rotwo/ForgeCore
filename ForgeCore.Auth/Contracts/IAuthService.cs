using ForgeCore.Shared.DTOs;

namespace ForgeCore.Auth.Contracts
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginGuestAsync();
        Task<AuthResultDto> RefreshAsync(string refreshToken);
        Task LogoutAsync(Guid sessionId);
        // Task LinkProviderAsync();
    }
}
