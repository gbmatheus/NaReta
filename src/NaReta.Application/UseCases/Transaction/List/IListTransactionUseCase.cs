using NaReta.Application.UseCases.Transaction._Common;

namespace NaReta.Application.UseCases.Transaction.List;

public interface IListTransactionUseCase
{
    Task<List<OutputTransaction>> ExecuteAsync(int accountId);
}
