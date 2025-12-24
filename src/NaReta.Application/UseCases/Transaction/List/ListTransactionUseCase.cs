using AutoMapper;
using NaReta.Application.DTO;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories.Transactions;
using NaReta.Domain.ValueObjects;

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

    public async Task<PaginationOutput<OutputTransaction>> ExecuteAsync(InputListTransaction input)
    {
        Validate(input);
        var filter = _mapper.Map<TransactionFilter>(input);

        var transactions = await _repository.ListByTransactionFilterAsync(filter);

        return _mapper.Map<PaginationOutput<OutputTransaction>>(transactions);
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
