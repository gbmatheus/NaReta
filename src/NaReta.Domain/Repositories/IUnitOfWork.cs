using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IAccountWriteOnlyRepository AccountWriteOnlyRepository { get; }
        ICategoryWriteOnlyRepository CategoryWriteOnlyRepository { get; }
        ITransactionWriteOnlyRepository TransactionWriteOnlyRepository { get; }
        Task CommitAsync();
    }
}
