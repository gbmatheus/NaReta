using AutoMapper;
using NaReta.Application.UseCases.Account.Create;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.UnitTest.Builder.Input;
using NaReta.UnitTest.Builder.Mapper;
using NaReta.UnitTest.Builder.Repository;
using Shouldly;

namespace NaReta.UnitTest.Application.UseCases.Account;

[Trait("Application", "Account UseCase")]
public class CreateAccountUseCaseTest
{
    [Fact(DisplayName = nameof(ExecuteAsync_ValidParameter_CreateTransaction))]
    public async Task ExecuteAsync_ValidParameter_CreateTransaction()
    {
        var input = InputAccountBuilder.Build();

        var useCase = CreateUseCase();
        var output = await useCase.ExecuteAsync(input);

        output.ShouldNotBeNull();
        output.Name.ShouldBe(input.Name);
        output.Email.ShouldBe(input.Email);
        output.Balance.ShouldBe(0);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenEmailExists_ThrowErrorOnValidationException))]
    public async Task ExecuteAsync_WhenEmailExists_ThrowErrorOnValidationException()
    {
        var input = InputAccountBuilder.Build();

        var useCase = CreateUseCase(input.Email);
        var act = async () => await useCase.ExecuteAsync(input);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_IN_USE);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenNameEmpty_ThrowErrorOnValidationException))]
    public async Task ExecuteAsync_WhenNameEmpty_ThrowErrorOnValidationException()
    {
        var input = InputAccountBuilder.Build();
        input.Name = string.Empty;

        var useCase = CreateUseCase();
        var act = async () => await useCase.ExecuteAsync(input);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.NAME_REQUIRED);
    }

    [Fact(DisplayName = nameof(ExecuteAsync_WhenEmailInvalid_ThrowErrorOnValidationException))]
    public async Task ExecuteAsync_WhenEmailInvalid_ThrowErrorOnValidationException()
    {
        var input = InputAccountBuilder.Build();
        input.Email = input.Name;

        var useCase = CreateUseCase();

        var act = async () => await useCase.ExecuteAsync(input);

        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldHaveSingleItem();
        result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_INVALID);
    }

    private static ICreateAccountUseCase CreateUseCase(string? email = null)
    {
        AccountWriteOnlyRepositoryBuilder accountRepositoryBuilder = new AccountWriteOnlyRepositoryBuilder();
        if (!string.IsNullOrWhiteSpace(email))
            accountRepositoryBuilder.ExistsByEmailAsync(email);
        IAccountWriteOnlyRepository accountRepository = accountRepositoryBuilder.Build();

        UnitOfWorkBuilder unitOfWorkBuilder = new UnitOfWorkBuilder();
        unitOfWorkBuilder.SetupAccountRepository(accountRepository);
        IUnitOfWork unitOfWork = unitOfWorkBuilder.Build();

        IMapper mapper = MapperBuilder.Build();

        var useCase = new CreateAccountUseCase(
            unitOfWork,
            mapper
            );

        return useCase;
    }
}
