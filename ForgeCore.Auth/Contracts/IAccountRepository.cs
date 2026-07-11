using ForgeCore.Auth.Domain;

namespace ForgeCore.Auth.Contracts
{
    public interface IAccountRepository
    {
        void Add(Account account);
        Task<Account?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        // Persist changes is handled by IUnitOfWork
    }
}
