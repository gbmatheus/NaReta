using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Transactions
{
    public interface ITransactionReadOnlyRepository
    {
        Task<List<Transaction>> ListAsync();
    }
}
