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

    public Expense(
        PaymentMethod paymentMethod,
        PaymentType paymentType,
        int installmentNumber,
        ExpenseType expenseType,
        List<Account> responsible
        )
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), paymentMethod))
            throw new DomainException("Payment method invalid");

        if (!Enum.IsDefined(typeof(PaymentType), paymentType))
            throw new DomainException("Payment type invalid");

        if (!Enum.IsDefined(typeof(ExpenseType), expenseType))
            throw new DomainException("Expense type invalid");

        if (PaymentType.Full.Equals(paymentType) && installmentNumber > 1)
            throw new DomainException("The payment method full is a single installment and cannot be divided into more installments");

        PaymentMethod = paymentMethod;
        PaymentType = paymentType;
        InstallmentNumber = installmentNumber;
        ExpenseType = expenseType;
        Responsibles = responsible;
    }

    public void AssignResponsibles (List<Account> responsibles)
    {
        Responsibles = responsibles;
    }
}
