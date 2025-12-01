using Bogus;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using NaReta.UnitTest.Builder.Entity;
using Shouldly;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var installmentNumber = faker.Random.Number(1);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible);

        expense.ShouldNotBeNull();
        expense.PaymentMethod.ShouldBe(method);
        expense.PaymentType.ShouldBe(type);
        expense.InstallmentNumber.ShouldBe(installmentNumber);
        expense.ExpenseType.ShouldBe(expenseType);
        expense.Responsibles.ShouldHaveSingleItem();
    }

    [Fact]
    public void Contructor_WhenPaymentMethodInvalid_ThrowDomainException()
    {
        var faker = new Faker();
        var method = (PaymentMethod) int.MaxValue;
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = faker.Random.Number(1);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible);

        act.ShouldThrow<DomainException>("Payment method invalid");
    }

    [Fact]
    public void Contructor_WhenPaymentTypeInvalid_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = (PaymentType)int.MaxValue;
        var installmentNumber = faker.Random.Number(1);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible);

        act.ShouldThrow<DomainException>("Payment type invalid");
    }

    [Fact]
    public void Contructor_WhenExpenseType_ThrowDomainException()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = faker.Random.Number(1);
        var expenseType = (ExpenseType)int.MaxValue;
        var responsible = new List<Account> { AccountEntityBuilder.Build() };

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible);

        act.ShouldThrow<DomainException>("Expense type invalid");
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

        var act = () => new Expense(method, type, installmentNumber, expenseType, responsible);

        act.ShouldThrow<DomainException>("The payment method full is a single installment and cannot be divided into more installments");
    }

    [Fact]
    public void AssignResponsibles_NewResposability_ChangeResponsability()
    {
        var faker = new Faker();
        var method = faker.PickRandom<PaymentMethod>();
        var type = faker.PickRandom<PaymentType>();
        var installmentNumber = faker.Random.Number(1);
        var expenseType = faker.PickRandom<ExpenseType>();
        var responsible = new List<Account> { AccountEntityBuilder.Build() };
        var newResponsible = new List<Account> { AccountEntityBuilder.Build() };

        var expense = new Expense(method, type, installmentNumber, expenseType, responsible);

        expense.AssignResponsibles(newResponsible);

        expense.Responsibles.ShouldHaveSingleItem();
        expense.Responsibles.ShouldBe(newResponsible);
    }
}
