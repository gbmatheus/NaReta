using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Repositories;
using System.Linq.Expressions;

namespace NaReta.Infra.DataAccess.Repositories;

internal class BaseRepository<T> : IBaseReadOnlyRepository<T>, IBaseWriteOnlyRepository<T> where T : class
{
    public readonly NaRetaDBContext _dbContext;

    public BaseRepository(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>().AsNoTracking().AsQueryable();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}
