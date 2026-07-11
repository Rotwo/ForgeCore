using ForgeCore.Auth.Domain;

namespace ForgeCore.Auth.Contracts
{
    public interface ISessionRepository
    {
        void Add(Session session);
        Task<Session?> GetByRefreshTokenAsync(string refreshToken);
        Task<Session?> GetByIdAsync(Guid id);
        Task RevokeAsync(Guid sessionId);
    }
}
