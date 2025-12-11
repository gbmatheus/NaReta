using Bogus;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using NaReta.UnitTest.Builder.Entity;
using Shouldly;

namespace NaReta.UnitTest.Domain.Entities;

[Trait("Domain", "Expense - Entity")]
public class ExpenseTest
{
    [Fact]
    public void Contructor_ValidParametes_CreateExpense()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        expense.ShouldNotBeNull();
        expense.PaymentMethod.ShouldBe(method);
        expense.PaymentType.ShouldBe(type);
        expense.InstallmentNumber.ShouldBe(installmentNumber);
        expense.ExpenseType.ShouldBe(expenseType);
        expense.Responsibles.ShouldHaveSingleItem();
        expense.Transaction.ShouldBe(transaction);
    }

    [Fact]
    public void Contructor_WhenPaymentMethodInvalid_ThrowDomainException()
    {
        var faker = new Faker();
        var method = (PaymentMethod)int.MaxValue;
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.PAYMENT_METHOD_INVALID);
    }

    [Fact]
    public void Contructor_WhenPaymentTypeInvalid_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = (PaymentType)int.MaxValue;
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }

    [Fact]
    public void Contructor_WhenExpenseType_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = 1;
        var expenseType = (ExpenseType)int.MaxValue;
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.EXPENSE_TYPE_INVALID);
    }

    [Fact]
    public void Contructor_WhenPaymentTypeFullAndInsallmentNumberGreaterThanOne_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Full;
        var installmentNumber = faker.Random.Number(2, 10);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.PAYMENT_METHOD_FULL_SINGLE);
    }

    [Fact]
    public void Contructor_WhenInstallmentNumberLessThanOne_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Installments;
        var installmentNumber = 0;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.INSTALLMENT_NUMBER_LESS_ONE);
    }

    [Fact]
    public void AssignResponsibles_NewResposability_ChangeResponsability()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };
        var newResponsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        expense.AssignResponsibles(newResponsible);

        expense.Responsibles.ShouldHaveSingleItem();
        expense.Responsibles.ShouldBe(newResponsible);
    }

    [Fact]
    public void Contructor_WhenTransactionTypeIncome_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Full;
        var installmentNumber = faker.Random.Number(2, 10);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Income);

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.TRANSACTION_NOT_EXPENSE);
    }

    [Fact]
    public void CalculateAmount_WhenIsFull_ReturnsTransacionAmount()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Full;
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        var amount = expense.CalculateAmount();

        amount.ShouldBe(transaction.Amount);
    }

    [Fact]
    public void CalculateAmount_WhenIsFullAndHasTwoResposible_ReturnsTransacionSplit()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Full;
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build(), AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        var amount = expense.CalculateAmount();

        amount.ShouldBe(transaction.Amount / responsible.Count);
    }

    [Fact]
    public void CalculateAmount_WhenNoResposible_ReturnsTransacionAmount()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Full;
        var installmentNumber = 1;
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        var amount = expense.CalculateAmount();

        amount.ShouldBe(transaction.Amount);
    }

    [Fact]
    public void CalculateAmount_WhenIsInstallments_ReturnsTransacionAmountSplit()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = PaymentType.Installments;
        var installmentNumber = faker.Random.Number(2, 4);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();
        var transaction = TransactionEntityBuilder.Build(account, category, TransactionType.Expense);

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible, transaction);

        var amount = expense.CalculateAmount();

        amount.ShouldBe(transaction.Amount / installmentNumber);
    }
}
