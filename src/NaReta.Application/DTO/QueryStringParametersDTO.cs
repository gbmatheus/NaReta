namespace NaReta.Application.DTO;

public abstract class QueryStringParametersDTO
{
    private int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    int _pageSize = 10;
    public int PageSize { get => _pageSize; set => _pageSize = (value > maxPageSize) ? maxPageSize : value; }
}
