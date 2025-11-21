using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Transactions
{
    public interface ITransactionReadOnlyRepository
    {
        Task<List<Transaction>> ListAsync();
        Task<List<Transaction>> ListByAccountIdAsync(int accountId, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int itemPerPage = 10); 
    }
}
