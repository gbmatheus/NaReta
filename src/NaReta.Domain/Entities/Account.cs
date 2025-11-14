using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities
{
    public class Account
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

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

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            CalculateBalance(Transactions);
        }
    }
}
