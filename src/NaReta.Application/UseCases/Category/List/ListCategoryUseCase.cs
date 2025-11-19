using AutoMapper;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Application.UseCases.Category.List;
internal class ListCategoryUseCase : IListCategoryUseCase
{
    private readonly ICategoryReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public ListCategoryUseCase(ICategoryReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OutputCategory>> ExecuteAsync()
    {
        var categories = await _repository.ListAsync();

        return _mapper.Map<List<OutputCategory>>(categories);
    }
}
