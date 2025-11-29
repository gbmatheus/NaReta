using AutoMapper;
using Bogus;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Domain.Repositories.Transactions;
using NaReta.UnitTest.Builder.Entity;
using NaReta.UnitTest.Builder.Mapper;
using NaReta.UnitTest.Builder.Repository;
using Shouldly;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.UnitTest.Application.UseCases.Transaction;

[Trait("Application", "Transaction - UseCase")]
public class ListTransactionUseCaseTest
{
    [Fact(DisplayName = nameof(ExecuteAsync_WhenNoTransactionsExists_ReturnsEmptyList))]
    public async Task ExecuteAsync_WhenNoTransactionsExists_ReturnsEmptyList()
    {
        int accountId = AccountEntityBuilder.Build().Id;

        var input = new InputListTransaction
        {
            AccountId = accountId
        };

        var useCase = CreateUseCase();

        var output = await useCase.ExecuteAsync(input);

        output.ShouldBeEmpty();
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenTransactionsExists_ReturnsTransactionList))]
    public async Task ExecuteAsync_WhenTransactionsExists_ReturnsTransactionList()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        int accountId = AccountEntityBuilder.Build().Id;

        var input = new InputListTransaction
        {
            AccountId = accountId
        };

        var count = new Faker().Random.Number(1, 20);

        var transactions = TransactionEntityBuilder.Build(account, category, count);

        var useCase = CreateUseCase(transactions);

        var output = await useCase.ExecuteAsync(input);

        output.ShouldNotBeEmpty();
        output.Count.ShouldBe(count);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenNoTransactionsExistsInRangeDate_ReturnsEmptyList))]
    public async Task ExecuteAsync_WhenNoTransactionsExistsInRangeDate_ReturnsEmptyList()
    {
        int accountId = AccountEntityBuilder.Build().Id;
        DateTime startDate = DateTime.UtcNow;
        DateTime endDate = DateTime.UtcNow;

        var input = new InputListTransaction
        {
            AccountId = accountId,
            StartDate = startDate,
            EndDate = endDate
        };

        var useCase = CreateUseCase();

        var output = await useCase.ExecuteAsync(input);

        output.ShouldBeEmpty();
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenTransactionsExistsInRangeDate_ReturnsTransactions))]
    public async Task ExecuteAsync_WhenTransactionsExistsInRangeDate_ReturnsTransactions()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        int accountId = AccountEntityBuilder.Build().Id;

        DateTime startDate = DateTime.UtcNow.AddDays(-1);
        DateTime endDate = DateTime.UtcNow;

        var input = new InputListTransaction
        {
            AccountId = accountId,
            StartDate = startDate,
            EndDate = endDate
        };

        var transaction1 = TransactionEntityBuilder.Build(account, category);
        transaction1.ChangeDate(DateTime.Today);

        var transaction2 = TransactionEntityBuilder.Build(account, category);
        transaction2.ChangeDate(DateTime.UtcNow.AddYears(-1));

        var useCase = CreateUseCase(new List<DomainEntity.Transaction> { transaction1, transaction2 }, input);

        var output = await useCase.ExecuteAsync(input);

        output.ShouldHaveSingleItem();
    }

    private static IListTransactionUseCase CreateUseCase(List<DomainEntity.Transaction>? transactions = null, InputListTransaction? input = null)
    {
        TransactionReadOnlyRepositoryBuilder transactionRepositoryBuilder = new TransactionReadOnlyRepositoryBuilder();
        if (input != null)
            transactionRepositoryBuilder.ListByAccountIdWithDateAsync(input, transactions!);
        else if (transactions != null && transactions.Count != 0)
            transactionRepositoryBuilder.ListByAccountIdAsync(transactions);

        ITransactionReadOnlyRepository repository = transactionRepositoryBuilder.Build();
        IMapper mapper = MapperBuilder.Build();

        var useCase = new ListTransactionUseCase(repository, mapper);

        return useCase;
    }
}

