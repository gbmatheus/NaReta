using NaReta.Application.DTO;
using NaReta.Application.UseCases.Transaction._Common;

namespace NaReta.Application.UseCases.Transaction.List;

public interface IListTransactionUseCase
{
    Task<PaginationOutput<OutputTransaction>> ExecuteAsync(InputListTransaction input);
}
