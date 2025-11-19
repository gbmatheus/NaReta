using AutoMapper;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.List;

internal class ListTransactionUseCase : IListTransactionUseCase
{
    private readonly ITransactionReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public ListTransactionUseCase(ITransactionReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OutputTransaction>> ExecuteAsync(int accountId)
    {
        var transactions = await _repository.ListByAccountIdAsync(accountId);

        return _mapper.Map<List<OutputTransaction>>(transactions);
    }
}
