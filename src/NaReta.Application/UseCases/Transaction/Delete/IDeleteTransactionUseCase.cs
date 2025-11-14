namespace NaReta.Application.UseCases.Transaction.Delete;

public interface IDeleteTransactionUseCase
{
    Task ExecuteAsync(int id);
}
