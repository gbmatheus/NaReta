using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities;

public class Account
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public Account() { }

    public Account(string name, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException(ResourceErrorMessages.EMAIL_EMPTY_OR_NULL);

        Name = name;
        Email = email;

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
