
using NaReta.Application.UseCases.Account._Common;
using NaReta.Domain;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.Application.UseCases.Account.Get;
internal class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IAccountReadOnlyRepository _repository;

    public GetAccountUseCase(
        IAccountReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<OutputAccount> ExecuteAsync(int id)
    {
        var account = await _repository.FindByIdAsync(id);

        if (account is null)
            // [TODO] NotFound
            throw new Exception(ResourceErrorMessages.ACCOUNT_NOT_FOUND);

        return new OutputAccount
        {
            Name = account.Name,
        };

    }
}
