using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using ForgeCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class AuthProviderRepository : IAuthProviderRepository
    {
        private readonly ForgeCoreDbContext _db;

        public AuthProviderRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public Task AddAsync(AuthProvider provider)
        {
            _db.AuthProviders.Add(provider);
            return _db.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(AuthProviderType type, string providerUserId)
        {
            return _db.AuthProviders.AnyAsync(p => p.Type == type && p.ProviderUserId == providerUserId);
        }

        public Task<List<AuthProvider>> GetByAccountIdAsync(Guid accountId)
        {
            return _db.AuthProviders.Where(p => p.AccountId == accountId).ToListAsync();
        }

        public Task<AuthProvider?> GetByProviderAsync(AuthProviderType type, string providerUserId)
        {
            return _db.AuthProviders
                .FirstOrDefaultAsync(p =>
                    p.Type == type &&
                    p.ProviderUserId == providerUserId);
        }
    }
}
