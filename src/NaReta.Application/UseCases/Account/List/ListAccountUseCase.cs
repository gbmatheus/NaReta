
using AutoMapper;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.Application.UseCases.Account.Create;

internal class ListAccountUseCase : IListAccountUseCase
{
    private readonly IAccountReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public ListAccountUseCase(IAccountReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OutputAccount>> ExecuteAsync()
    {
        var account = await _repository.ListAsync();

        return _mapper.Map<List<OutputAccount>>(account);
    }
}
