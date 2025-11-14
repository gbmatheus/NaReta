using NaReta.Domain.Enums;

namespace NaReta.Application.UseCases.Transaction.Create;

public class InputCreateTransaction
{
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}
