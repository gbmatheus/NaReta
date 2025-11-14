using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Infra.DataAccess.Repositories;
internal class TransactionRepository : ITransactionReadOnlyRepository, ITransactionWriteOnlyRepository
{
    private readonly NaRetaDBContext _dbContext;

    public TransactionRepository(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSync(Transaction transaction)
    {
        await _dbContext.transactions.AddAsync(transaction);
    }

    public async Task<Transaction?> FindByIdAsync(int id)
    {
        return await _dbContext.transactions.FindAsync(id);
    }

    public async Task<List<Transaction>> ListAsync()
    {
        // incluir em um commit diferente
        return await _dbContext.transactions.Include(t => t.Category).AsNoTracking().ToListAsync();
    }

    public void Remove(Transaction transaction)
    {
        _dbContext.transactions.Remove(transaction);
    }

    public void Update(Transaction transaction)
    {
        _dbContext.transactions.Update(transaction);
    }
}
