using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public TransactionTypeEnum Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Category Category { get; private set; }

        public Transaction(TransactionTypeEnum type, decimal amount, DateTime date, Category category, string description)
        {
            if (Enum.IsDefined(typeof(TransactionTypeEnum), type) is false)
                throw new ArgumentException(ResourceErrorMessages.TYPE_INVALID);

            if (amount <= 0)
                throw new ArgumentException(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);

            Type = type;
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
        }
    }
}
