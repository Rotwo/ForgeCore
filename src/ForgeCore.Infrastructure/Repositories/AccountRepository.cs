using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using ForgeCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ForgeCoreDbContext _db;

        public AccountRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public void Add(Account account)
        {
            _db.Accounts.Add(account);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _db.Accounts.AnyAsync(a => a.Id == id);
        }

        public Task<Account?> GetByIdAsync(Guid id)
        {
            return _db.Accounts
                .Include(a => a.AuthProviders)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Persistence is handled by UnitOfWork
    }
}
