using ForgeCore.Auth.Domain;

namespace ForgeCore.Auth.Contracts
{
    public interface IAuthProviderRepository
    {
        void Add(AuthProvider provider);
        Task<AuthProvider?> GetByProviderAsync(AuthProviderType type, string providerUserId);
        Task<bool> ExistsAsync(AuthProviderType type, string providerUserId);
        Task<List<AuthProvider>> GetByAccountIdAsync(Guid accountId);
    }
}
