using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        public Transaction(int accountId, TransactionType type, decimal amount, DateTime date, Category category, string description)
        {
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
            Validate();
        }

        public void ApplyChanges(TransactionType type, decimal amount, DateTime date, Category category, string description)
        {
            Type = type;
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
            Validate();
        }

        public void ChangeType(TransactionType type)
        {
            Type = type;
            Validate();
        }

        public void ChangeAmount(decimal amount)
        {
            Amount = amount;
            Validate();
        }

        public void ChangeDate(DateTime date)
        {
            Date = date;
            Validate();
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            Validate();
        }

        public void ChangeCategory(Category category)
        {
            Category = category;
            Validate();
        }

        private void Validate()
        {
            if (Enum.IsDefined(typeof(TransactionType), Type) is false)
                throw new ArgumentException(ResourceErrorMessages.TYPE_INVALID);

            if (Amount <= 0)
                throw new ArgumentException(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);
        }
    }
}
