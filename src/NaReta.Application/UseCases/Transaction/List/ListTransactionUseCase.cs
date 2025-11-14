using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.List;

internal class ListTransactionUseCase : IListTransactionUseCase
{
    private readonly ITransactionReadOnlyRepository _repository;

    public ListTransactionUseCase(ITransactionReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OutputTransaction>> ExecuteAsync()
    {
        var transactions = await _repository.ListAsync();

        var output = new List<OutputTransaction>();
        foreach (var transaction in transactions)
        {
            output.Add(new OutputTransaction
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Type = transaction.Type,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Description = transaction.Description,
                Category = transaction.Category.Name
            });
        }

        return output;
    }
}
