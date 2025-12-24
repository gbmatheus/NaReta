using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Categories
{
    public interface ICategoryReadOnlyRepository: IBaseReadOnlyRepository<Category>
    {
        Task<List<Category>> ListAsync();
    }
}
