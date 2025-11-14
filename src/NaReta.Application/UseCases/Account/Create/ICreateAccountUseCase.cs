using NaReta.Application.UseCases.Account._Common;

namespace NaReta.Application.UseCases.Account.Create;

public interface ICreateAccountUseCase
{
    Task<OutputAccount> ExecuteAsync(InputCreateAccount input);
}
