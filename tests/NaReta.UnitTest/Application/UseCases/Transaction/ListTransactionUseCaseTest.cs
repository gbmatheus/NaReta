using AutoMapper;
using Bogus;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Common;
using NaReta.Common.Exceptions;
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
    const int ACCOUNT_ID = 1;

    [Fact(DisplayName = nameof(ExecuteAsync_GivenNoTransactionsExists_ReturnsEmptyList))]
    public async Task ExecuteAsync_GivenNoTransactionsExists_ReturnsEmptyList()
    {
        const int TOTAL_COUNT = 0;
        var input = new InputListTransaction
        {
            AccountId = ACCOUNT_ID
        };

        var useCase = CreateUseCase([], input, TOTAL_COUNT);
        var output = await useCase.ExecuteAsync(input);
        output.Items.ShouldBeEmpty();
        output.TotalCount.ShouldBe(TOTAL_COUNT);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_GivenTransactionsExists_ReturnsTransactionList))]
    public async Task ExecuteAsync_GivenTransactionsExists_ReturnsTransactionList()
    {
        const int PAGE_NUMBER = 1;
        const int PAGE_SIZE = 10;

        var input = new InputListTransaction
        {
            AccountId = ACCOUNT_ID,
            PageNumber = PAGE_NUMBER,
            PageSize = PAGE_SIZE
        };

        var totalCount = new Faker().Random.Number(1, 20);
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transactions = TransactionEntityBuilder.Build(account, category, totalCount);

        var useCase = CreateUseCase(transactions, input, totalCount);

        var output = await useCase.ExecuteAsync(input);

        output.Items.ShouldNotBeEmpty();
        output.Items.ToList().Count.ShouldBe(transactions.Count);
        output.TotalCount.ShouldBe(totalCount);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_GivenNoTransactionsExistsInRangeDate_ReturnsEmptyList))]
    public async Task ExecuteAsync_GivenNoTransactionsExistsInRangeDate_ReturnsEmptyList()
    {
        const int TOTAL_COUNT = 0;

        DateTime startDate = DateTime.UtcNow.Date;
        DateTime endDate = DateTime.UtcNow.Date;

        var input = new InputListTransaction
        {
            AccountId = ACCOUNT_ID,
            StartDate = startDate,
            EndDate = endDate
        };

        var useCase = CreateUseCase([], input, TOTAL_COUNT);

        var output = await useCase.ExecuteAsync(input);

        output.Items.ShouldBeEmpty();
        output.TotalCount.ShouldBe(TOTAL_COUNT);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_GivenTransactionsExistsInRangeDate_ReturnsTransactions))]
    public async Task ExecuteAsync_GivenTransactionsExistsInRangeDate_ReturnsTransactions()
    {
        DateTime startDate = DateTime.UtcNow.Date.AddDays(-2);
        DateTime endDate = DateTime.UtcNow.Date.AddDays(1);

        var input = new InputListTransaction
        {
            AccountId = ACCOUNT_ID,
            StartDate = startDate,
            EndDate = endDate,
            PageSize = new Faker().Random.Number(1, 100),
            PageNumber = 1
        };
        var totalCount = new Faker().Random.Number(1, 20);
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transactions = TransactionEntityBuilder.Build(account, category, totalCount);

        var useCase = CreateUseCase(transactions, input, totalCount);

        var output = await useCase.ExecuteAsync(input);

        output.Items.ToList().Count.ShouldBe(transactions.Count);
        output.TotalCount.ShouldBe(totalCount);
    }

    [Theory(DisplayName = nameof(ExecuteAsync_GivenTransactionsExistsInRangeDate_ReturnsTransactions))]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    public async Task ExecuteAsync_GivenItemPerPage_ReturnsTransactions(int pageSize)
    {
        const int PAGE_NUMBER = 1;

        var input = new InputListTransaction
        {
            AccountId = ACCOUNT_ID,
            PageSize = pageSize,
            PageNumber = PAGE_NUMBER
        };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transactions = TransactionEntityBuilder.Build(account, category, pageSize);

        var useCase = CreateUseCase(transactions, input, pageSize);

        var output = await useCase.ExecuteAsync(input);

        output.TotalCount.ShouldBe(pageSize);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_GivenInputWithAccountIdZero_ThrowErrorOnValidationException))]
    public async Task ExecuteAsync_GivenInputWithAccountIdZero_ThrowErrorOnValidationException()
    {
        var input = new InputListTransaction
        {
            AccountId = 0,
        };
        var useCase = CreateUseCase([], input, 0);

        var act = async () => await useCase.ExecuteAsync(input);

        await act.ShouldThrowAsync<ErrorOnValidationException>(ResourceErrorMessages.ACCOUNT_INVALID);
    }

    private static IListTransactionUseCase CreateUseCase(List<DomainEntity.Transaction> transactions, InputListTransaction input, int totalCount)
    {
        TransactionReadOnlyRepositoryBuilder transactionRepositoryBuilder = new TransactionReadOnlyRepositoryBuilder();
        transactionRepositoryBuilder.ListByTransactionFilterAsync(input, transactions, totalCount);

        ITransactionReadOnlyRepository repository = transactionRepositoryBuilder.Build();
        IMapper mapper = MapperBuilder.Build();

        var useCase = new ListTransactionUseCase(repository, mapper);
        
        return useCase;
    }
}

