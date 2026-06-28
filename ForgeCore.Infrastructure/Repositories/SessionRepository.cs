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

        public Task AddAsync(Session session)
        {
            _db.Sessions.Add(session);
            return _db.SaveChangesAsync();
        }

        public Task<Session?> GetByIdAsync(Guid id)
        {
            return _db.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Session?> GetByRefreshTokenAsync(string refreshToken)
        {
            return _db.Sessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
        }

        public Task RevokeAsync(Guid sessionId)
        {
            return _db.Sessions.Where(s => s.Id == sessionId).ExecuteDeleteAsync();
        }
    }
}
