using System.Data;

namespace ForgeCore.Shared.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(IsolationLevel isolationLevel);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
