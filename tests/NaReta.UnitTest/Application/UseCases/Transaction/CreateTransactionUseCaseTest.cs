using AutoMapper;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Application.UseCases.Transactions.Create;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;
using NaReta.UnitTest.Builder.Entity;
using NaReta.UnitTest.Builder.Input;
using NaReta.UnitTest.Builder.Mapper;
using NaReta.UnitTest.Builder.Repository;
using Shouldly;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.UnitTest.Application.UseCases.Transaction;

[Trait("Application", "Transaction - UseCase")]
public class CreateTransactionUseCaseTest
{
    [Fact(DisplayName = nameof(ExecuteAsync_ValidParameter_CreateTransaction))]
    public async Task ExecuteAsync_ValidParameter_CreateTransaction()
    {
        var account = AccountEntityBuilder.Build();
        int accountId = account.Id;

        var category = CategoryEntityBuilder.Build();

        var input = InputTransactionBuilder.Build();
        input.CategoryId = category.Id;

        var useCase = CreateUseCase(account, category);

        var output = await useCase.ExecuteAsync(accountId, input);


        output.ShouldNotBeNull();
        output.Amount.ShouldBe(input.Amount);
        output.Date.ShouldBe(input.Date);
        output.Description.ShouldBe(input.Description);
        output.Category.ShouldBe(category.Name);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenAccountNotFound_ThrowNotFoundException))]
    public async Task ExecuteAsync_WhenAccountNotFound_ThrowNotFoundException()
    {
        int accountId = AccountEntityBuilder.Build().Id;
        var category = CategoryEntityBuilder.Build();

        var input = InputTransactionBuilder.Build();
        input.CategoryId = category.Id;

        var useCase = CreateUseCase(category: category);

        var act = async () => await useCase.ExecuteAsync(accountId, input);

        var result = await act.ShouldThrowAsync<NotFoundException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.ACCOUNT_NOT_FOUND);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenAccountNotFound_ThrowNotFoundException))]
    public async Task ExecuteAsync_WhenCategoryNotFound_ThrowNotFoundException()
    {
        var account = AccountEntityBuilder.Build();
        int accountId = account.Id;

        var input = InputTransactionBuilder.Build();

        var useCase = CreateUseCase(account);

        var act = async () => await useCase.ExecuteAsync(accountId, input);

        var result = await act.ShouldThrowAsync<NotFoundException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.CATEGORY_NOT_FOUND);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenDescriptionEmpty_ThrowErrorOnValidatonException))]
    public async Task ExecuteAsync_WhenDescriptionEmpty_ThrowErrorOnValidatonException()
    {
        var account = AccountEntityBuilder.Build();
        int accountId = account.Id;

        var category = CategoryEntityBuilder.Build();

        var input = InputTransactionBuilder.Build();
        input.Description = string.Empty;

        var useCase = CreateUseCase(account, category);

        var act = async () => await useCase.ExecuteAsync(accountId, input);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.DESCRIPTION_INVALID);
    }

    private static ICreateTransactionUseCase CreateUseCase(DomainEntity.Account? account = null, DomainEntity.Category? category = null)
    {
        AccountWriteOnlyRepositoryBuilder accountRepositoryBuilder = new AccountWriteOnlyRepositoryBuilder();
        if (account != null)
            accountRepositoryBuilder.FindByIdAsync(account);
        IAccountWriteOnlyRepository accountRepository = accountRepositoryBuilder.Build();

        CategoryWriteOnlyRepositoryBuilder categoryRepositoryBuilder = new CategoryWriteOnlyRepositoryBuilder();
        if (category != null)
            categoryRepositoryBuilder.FindByIdAsync(category);
        ICategoryWriteOnlyRepository categoryRepository = categoryRepositoryBuilder.Build();

        ITransactionWriteOnlyRepository transactionRepository = TransactionWriteOnlyRepositoryBuilder.Build();
        IUnitOfWork unitOfWork = IUnitOfWorkBuilder.Build(
            accountRepository,
            categoryRepository,
            transactionRepository);
        IMapper mapper = MapperBuilder.Build();

        var useCase = new CreateTransactionUseCase(
            unitOfWork,
            mapper);

        return useCase;
    }


}
