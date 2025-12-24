using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities;

public class Expense
{
    public int Id { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public int InstallmentNumber { get; private set; }
    public ExpenseType ExpenseType { get; private set; }
    public List<string> Responsibles { get; private set; } = [];
    public Transaction Transaction { get; private set; }

    public Expense()
    {
    }

    public Expense(
        PaymentMethod paymentMethod,
        PaymentType paymentType,
        int installmentNumber,
        ExpenseType expenseType,
        List<string> responsible,
        Transaction transaction
        )
    {
        PaymentMethod = paymentMethod;
        PaymentType = paymentType;
        InstallmentNumber = installmentNumber;
        ExpenseType = expenseType;
        Responsibles = responsible;
        Transaction = transaction;

        Validate();
    }

    public void AssignResponsibles(List<string> responsibles)
    {
        Responsibles = responsibles;
    }

    public decimal CalculateAmountPerResponsible()
    {
        if (Responsibles.Count != 0)
            return Transaction.Amount / Responsibles.Count;
        return Transaction.Amount;
    }

    private void Validate()
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), PaymentMethod))
            throw new DomainException(ResourceErrorMessages.PAYMENT_METHOD_INVALID);

        if (!Enum.IsDefined(typeof(PaymentType), PaymentType))
            throw new DomainException(ResourceErrorMessages.PAYMENT_TYPE_INVALID);

        if (InstallmentNumber <= 0)
            throw new DomainException(ResourceErrorMessages.INSTALLMENT_NUMBER_LESS_ONE);

        if (!Enum.IsDefined(typeof(ExpenseType), ExpenseType))
            throw new DomainException(ResourceErrorMessages.EXPENSE_TYPE_INVALID);

        if (PaymentType.Full.Equals(PaymentType) && InstallmentNumber > 1)
            throw new DomainException(ResourceErrorMessages.PAYMENT_METHOD_FULL_SINGLE);
    }
}
