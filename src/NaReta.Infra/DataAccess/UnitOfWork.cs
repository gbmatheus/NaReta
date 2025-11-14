using NaReta.Domain.Repositories;

namespace NaReta.Infra.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly NaRetaDBContext _dbContext;

    public UnitOfWork(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
