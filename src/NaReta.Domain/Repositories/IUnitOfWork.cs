namespace NaReta.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
