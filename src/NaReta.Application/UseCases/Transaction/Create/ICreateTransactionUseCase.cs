using NaReta.Application.UseCases.Transaction._Common;

namespace NaReta.Application.UseCases.Transaction.Create;
public interface ICreateTransactionUseCase
{
    Task<OutputTransaction> ExecuteAsync(int accountId, InputTransaction input);
}
