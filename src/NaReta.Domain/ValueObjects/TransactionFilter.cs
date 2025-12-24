namespace NaReta.Domain.ValueObjects;

public class TransactionFilter: BaseFilter
{
    public int AccountId { get; set; }
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
}
