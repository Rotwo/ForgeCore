using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using ForgeCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ForgeCoreDbContext _db;

        public SessionRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public void Add(Session session)
        {
            _db.Sessions.Add(session);
        }

        public Task<Session?> GetByIdAsync(Guid id)
        {
            return _db.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Session?> GetByRefreshTokenAsync(string refreshToken)
        {
            return _db.Sessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
        }

        public async Task RevokeAsync(Guid sessionId)
        {
            var session = await _db.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (session is null) return;
            session.Revoke();
        }
    }
}
