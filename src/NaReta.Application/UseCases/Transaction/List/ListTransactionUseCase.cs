using AutoMapper;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.List;

public class ListTransactionUseCase : IListTransactionUseCase
{
    private readonly ITransactionReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public ListTransactionUseCase(ITransactionReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OutputTransaction>> ExecuteAsync(InputListTransaction input)
    {
        var transactions = await _repository.ListByAccountIdAsync(input.AccountId, input.StartDate, input.EndDate);

        return _mapper.Map<List<OutputTransaction>>(transactions);
    }

    public void Validate(InputListTransaction input)
    {
        var validator = new InputListTransactionValidator();
        var result = validator.Validate(input);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
