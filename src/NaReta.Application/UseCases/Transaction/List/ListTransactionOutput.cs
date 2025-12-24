using NaReta.Application.UseCases.Transaction._Common;

namespace NaReta.Application.UseCases.Transaction.List;

public class ListTransactionOutput
{
    public IEnumerable<OutputTransaction> Items { get; set; } = [];
}
