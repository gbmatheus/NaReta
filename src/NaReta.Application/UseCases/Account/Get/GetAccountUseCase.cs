
using AutoMapper;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Account.Get;

internal class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IAccountReadOnlyRepository _repository;
    private readonly ITransactionReadOnlyRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetAccountUseCase(IAccountReadOnlyRepository repository, ITransactionReadOnlyRepository transactionRepository, IMapper mapper)
    {
        _repository = repository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<OutputAccount> ExecuteAsync(int id)
    {
        var account = await _repository.FindByIdAsync(id);

        if (account is null)
            throw new NotFoundException(ResourceErrorMessages.ACCOUNT_NOT_FOUND);

        // Error - Balanço com o ultimo valor da transação
        //account.CalculateBalance();

        return _mapper.Map<OutputAccount>(account);

    }
}
