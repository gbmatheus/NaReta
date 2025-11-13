using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities
{
    public class Account
    {
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
            Name = name;
        }

        public void CalculateBalance(List<Transaction> transactions)
        {
            Balance = transactions.Sum(transaction => transaction.Type is TransactionType.Income 
                ? transaction.Amount
                : transaction.Amount * -1
            );
        }
    }
}
