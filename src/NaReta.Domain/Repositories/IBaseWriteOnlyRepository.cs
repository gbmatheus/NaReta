using System.Linq.Expressions;

namespace NaReta.Domain.Repositories;

public interface IBaseWriteOnlyRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    void Update(T entity);
    void Delete(T entity);
}
