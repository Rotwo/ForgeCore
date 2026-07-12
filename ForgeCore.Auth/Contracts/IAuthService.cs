using ForgeCore.Auth.Domain;
using ForgeCore.Shared.DTOs;

namespace ForgeCore.Auth.Contracts
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginGuestAsync();
        Task<AuthResultDto> RefreshAsync(string refreshToken);
        Task LogoutAsync(Guid sessionId);

        Task<AuthResultDto> LoginAsync(AuthProviderType providerType, string credentials);
        Task<AuthResultDto> RegisterEmailAsync(string email, string password, string displayName);
        Task LinkProviderAsync(Guid accountId, AuthProviderType providerType, string credentials);
    }
}
