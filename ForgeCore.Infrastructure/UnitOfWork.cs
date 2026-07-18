using ForgeCore.Infrastructure.Persistence;
using ForgeCore.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ForgeCore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForgeCoreDbContext _db;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_currentTransaction != null)
            {
                return await operation();
            }

            _currentTransaction = await _db.Database.BeginTransactionAsync(isolationLevel);
            try
            {
                var result = await operation();
                await _currentTransaction.CommitAsync();
                return result;
            }
            catch
            {
                await _currentTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(IsolationLevel isolationLevel)
        {
            if (_currentTransaction != null)
            {
                return await _db.SaveChangesAsync();
            }

            _currentTransaction = await _db.Database.BeginTransactionAsync(isolationLevel);
            try
            {
                var result = await _db.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
                return result;
            }
            catch
            {
                await _currentTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
