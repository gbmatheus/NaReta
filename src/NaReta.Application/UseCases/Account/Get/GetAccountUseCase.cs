
using NaReta.Application.UseCases.Account._Common;
using NaReta.Common;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Account.Get;

internal class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IAccountReadOnlyRepository _repository;
    private readonly ITransactionReadOnlyRepository _transactionRepository;

    public GetAccountUseCase(
        IAccountReadOnlyRepository repository,
        ITransactionReadOnlyRepository transactionRepository)
    {
        _repository = repository;
        _transactionRepository = transactionRepository;
    }

    public async Task<OutputAccount> ExecuteAsync(int id)
    {
        var account = await _repository.FindByIdAsync(id);

        if (account is null)
            // [TODO] NotFound
            throw new Exception(ResourceErrorMessages.ACCOUNT_NOT_FOUND);

        // Error - Balanço com o ultimo valor da transação
        //account.CalculateBalance();

        var outputTransaction = new List<OutputTransactionIntoAccount>();
        account.Transactions.ForEach(transaction =>
        {
            outputTransaction.Add(new OutputTransactionIntoAccount
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type.ToString(),
                Category = transaction.Category.Name
            });
        });

        return new OutputAccount
        {
            Name = account.Name,
            Balance = account.Balance,
            Transactions = outputTransaction
        };

    }
}
