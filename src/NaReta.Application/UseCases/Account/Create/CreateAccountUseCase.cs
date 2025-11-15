using FluentValidation.Results;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Account.Create;

internal class CreateAccountUseCase : ICreateAccountUseCase
{
    private readonly IAccountWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountUseCase(
        IAccountWriteOnlyRepository accountWriteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = accountWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputAccount> ExecuteAsync(InputCreateAccount input)
    {
        await Validate(input);

        var account = new DomainEntity.Account(input.Name);
        await _repository.AddAsync(account);
        await _unitOfWork.Commit();

        return new OutputAccount
        {
            Name = account.Name,
        };
    }

    private async Task Validate(InputCreateAccount input)
    {
        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        var accountExists = await _repository.ExistsByNameAsync(input.Name);

        if (accountExists)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.ACCOUNT_NAME_IN_USE));

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
