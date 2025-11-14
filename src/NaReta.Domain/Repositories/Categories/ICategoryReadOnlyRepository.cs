using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Categories
{
    public interface ICategoryReadOnlyRepository
    {
        Task<List<Category>> ListAsync();
    }
}
