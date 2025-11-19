using NaReta.Application.UseCases.Account._Common;

namespace NaReta.Application.UseCases.Account.Create;

public interface IListAccountUseCase
{
    Task<List<OutputShortAccount>> ExecuteAsync();
}
