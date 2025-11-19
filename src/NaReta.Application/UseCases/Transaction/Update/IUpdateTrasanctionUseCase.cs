using NaReta.Application.UseCases.Transaction._Common;

namespace NaReta.Application.UseCases.Transaction.Update;
public interface IUpdateTrasanctionUseCase
{
    Task<OutputTransaction> ExecuteAsync(int id, InputTransaction input);
}
