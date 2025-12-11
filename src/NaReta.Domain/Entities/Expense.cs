using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Enums;

namespace NaReta.Domain.Entities;

public class Expense
{
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public int InstallmentNumber { get; private set; }
    public ExpenseType ExpenseType { get; private set; }
    public List<Account> Responsibles { get; private set; }
    public Transaction Transaction { get; private set; }

    public Expense(
        PaymentMethod paymentMethod,
        PaymentType paymentType,
        int installmentNumber,
        ExpenseType expenseType,
        List<Account> responsible,
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

    public void AssignResponsibles(List<Account> responsibles)
    {
        Responsibles = responsibles;
    }

    public decimal CalculateAmount()
    {
        decimal amount = Transaction.Amount / InstallmentNumber;
        if (Responsibles.Count != 0)
            amount = amount / Responsibles.Count;
        return amount;
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
