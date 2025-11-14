using NaReta.Application.UseCases.Account._Common;

namespace NaReta.Application.UseCases.Account.Create;

public interface IListAccountUseCase
{
    Task<List<OutputAccount>> ExecuteAsync();
}
