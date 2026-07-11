using ForgeCore.Shared.Contracts;
using ForgeCore.Infrastructure.Persistence;

namespace ForgeCore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForgeCoreDbContext _db;

        public UnitOfWork(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
