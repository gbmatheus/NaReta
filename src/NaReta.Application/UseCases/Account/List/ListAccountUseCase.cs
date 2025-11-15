
using NaReta.Application.UseCases.Account._Common;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.Application.UseCases.Account.Create;
internal class ListAccountUseCase : IListAccountUseCase
{
    private readonly IAccountReadOnlyRepository _repository;

    public ListAccountUseCase(
        IAccountReadOnlyRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<List<OutputAccount>> ExecuteAsync()
    {
        var accountExists = await _repository.ListAsync();

        var output = new List<OutputAccount>();

        foreach (var account in accountExists)
        {
            output.Add(new OutputAccount
            {
                Id = account.Id,
                Name = account.Name,
            });
        }

        return output;

    }
}
