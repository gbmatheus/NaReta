using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Transactions
{
    public interface ITransactionWriteOnlyRepository
    {
        Task AddSync(Transaction transaction);
        Task<Transaction?> FindByIdAsync(int id);
        void Update(Transaction transaction);
        void Remove(Transaction transaction);
    }
}
