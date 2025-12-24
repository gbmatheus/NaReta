using NaReta.Common.Exceptions;

namespace NaReta.Domain.ValueObjects;

public class PagedResult<T>
{
    private const int MaxPageSize = 100;
    public IEnumerable<T> Items { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) throw new DomainException("Página deve ser maior que zero.");
        if (pageSize <= 0 || pageSize > MaxPageSize) throw new DomainException("Tamanho de página inválido.");

        Items = items;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
