using NaReta.Common;
using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities;

public class Account
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public Account() { }

    public Account(string name, List<Transaction>? transactions = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
        Name = name;

        Transactions = transactions ?? new List<Transaction>();
        CalculateBalance();
    }

    public void CalculateBalance()
    {
        Balance = Transactions.Sum(transaction => transaction.Type is TransactionType.Income
            ? transaction.Amount
            : transaction.Amount * -1
        );
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
        CalculateBalance();
    }
}
