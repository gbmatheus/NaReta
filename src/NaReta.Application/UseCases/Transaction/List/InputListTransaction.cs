namespace NaReta.Application.UseCases.Transaction.List;

public class InputListTransaction
{
    public int AccountId { get; set; }
    public int ItemPerPage { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
}
