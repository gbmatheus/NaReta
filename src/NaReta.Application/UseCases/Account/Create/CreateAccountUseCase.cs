using AutoMapper;
using FluentValidation.Results;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Account.Create;

public class CreateAccountUseCase : ICreateAccountUseCase
{
    private readonly IAccountWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAccountUseCase(IAccountWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OutputAccount> ExecuteAsync(InputCreateAccount input)
    {
        await ValidateAsync(input);

        var account = new DomainEntity.Account(input.Name, input.Email);
        await _repository.AddAsync(account);
        await _unitOfWork.Commit();

        return _mapper.Map<OutputAccount>(account);
    }

    private async Task ValidateAsync(InputCreateAccount input)
    {
        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        var accountExists = await _repository.ExistsByEmailAsync(input.Email);

        if (accountExists)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_IN_USE));

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
