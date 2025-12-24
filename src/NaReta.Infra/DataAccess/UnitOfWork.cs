using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;
using NaReta.Infra.DataAccess.Repositories;

namespace NaReta.Infra.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly NaRetaDBContext _dbContext;

    private AccountRepository? _accountRepository;
    private CategoryRepository? _categoryRepository;
    private TransactionRepository? _transactionRepository;

    public UnitOfWork(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IAccountWriteOnlyRepository AccountWriteOnlyRepository
        => _accountRepository ??= new AccountRepository(_dbContext);

    public ICategoryWriteOnlyRepository CategoryWriteOnlyRepository
        => _categoryRepository ??= new CategoryRepository(_dbContext);

    public ITransactionWriteOnlyRepository TransactionWriteOnlyRepository
        => _transactionRepository ??= new TransactionRepository(_dbContext);

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
