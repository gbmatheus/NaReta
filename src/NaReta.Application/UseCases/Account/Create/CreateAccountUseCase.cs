
using NaReta.Application.UseCases.Account._Common;
using NaReta.Domain;
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
        var accountExists = await _repository.ExistsByNameAsync(input.Name);

        if (accountExists)
            // [TODO] BadRequest
            throw new Exception(ResourceErrorMessages.ACCOUNT_NAME_IN_USE);

        var account = new DomainEntity.Account(input.Name);
        await _repository.AddAsync(account);
        await _unitOfWork.Commit();

        return new OutputAccount
        {
            Name = account.Name,
        };

    }
}
