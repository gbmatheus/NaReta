using NaReta.Domain.Enums;

namespace NaReta.Application.UseCases.Transaction._Common;
public class OutputTransaction
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}
