using System.Linq.Expressions;

namespace NaReta.Domain.Repositories;

public interface IBaseReadOnlyRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
}
