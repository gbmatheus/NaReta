using NaReta.Domain.Enums;

namespace NaReta.Application.UseCases.Transaction.Update;

public class InputUpdateTransaction
{
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int CategoryId { get; set; }
}
