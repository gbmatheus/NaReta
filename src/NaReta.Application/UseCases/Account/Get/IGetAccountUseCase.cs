using NaReta.Application.UseCases.Account._Common;

namespace NaReta.Application.UseCases.Account.Get;

public interface IGetAccountUseCase
{
    Task<OutputAccount> ExecuteAsync(int id);
}
