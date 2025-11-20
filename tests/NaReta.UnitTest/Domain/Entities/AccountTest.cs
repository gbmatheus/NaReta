using Bogus;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using NaReta.UnitTest.Builder.Entity;
using Shouldly;

namespace NaReta.UnitTest.Domain.Entities;

[Trait("Domain", "Entity - Account")]
public class AccountTest
{
    [Fact(DisplayName = nameof(Contructor_ValidParameters_CreateAccount))]
    public void Contructor_ValidParameters_CreateAccount()
    {
        var faker = new Faker();
        var name = faker.Person.FirstName;
        var email = faker.Person.Email;
        
        var account = new Account(
            name,
            email
        );

        account.ShouldNotBeNull();
        account.Name.ShouldBe(name);
        account.Email.ShouldBe(email);
        account.Balance.ShouldBe(0);
        account.Transactions.ShouldBeEmpty();
    }

    [Theory(DisplayName = nameof(Contructor_WhenNameEmptyOrNull_ThrowDomainException))]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Contructor_WhenNameEmptyOrNull_ThrowDomainException(string name)
    {
        var faker = new Faker();
        var email = faker.Person.Email;

        var act = () => new Account(
            name,
            email
        );

        act.ShouldThrow<DomainException>(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
    }

    [Theory(DisplayName = nameof(Contructor_WhenEmailEmptyOrNull_ThrowDomainException))]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Contructor_WhenEmailEmptyOrNull_ThrowDomainException(string email)
    {
        var faker = new Faker();
        var name = faker.Person.FirstName;

        var act = () => new Account(
            name,
            email
        );

        act.ShouldThrow<DomainException>(ResourceErrorMessages.EMAIL_EMPTY_OR_NULL);
    }

    [Fact(DisplayName = nameof(CalculateBalance_WhenTransactionIncome_ReturnsBalancePositive))]
    public void CalculateBalance_WhenTransactionIncome_ReturnsBalancePositive()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();

        var faker = new Faker();
        TransactionType TYPE = TransactionType.Income;
        decimal amount = faker.Finance.Amount(1);
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Receita 1";

        var transcation1 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);

        account.Balance.ShouldBe(transcation1.Amount + transcation2.Amount);
    }

    [Fact(DisplayName = nameof(CalculateBalance_WhenTransactionExpense_ReturnsBalanceNegative))]
    public void CalculateBalance_WhenTransactionExpense_ReturnsBalanceNegative()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();

        var faker = new Faker();
        TransactionType TYPE = TransactionType.Expense;
        decimal amount = faker.Finance.Amount(1);
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Despesa 1";

        var transcation1 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);

        account.Balance.ShouldBe(0 - (transcation1.Amount + transcation2.Amount));
    }

    [Fact(DisplayName = nameof(CalculateBalance_WhenTransactionIncomeGreaterThanExpense_ReturnsBalancePositive))]
    public void CalculateBalance_WhenTransactionIncomeGreaterThanExpense_ReturnsBalancePositive()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();

        var faker = new Faker();
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Despesa 1";

        var transcation1 = new Transaction(account, TransactionType.Income, faker.Finance.Amount(100, 200), date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TransactionType.Expense, faker.Finance.Amount(1, 100), date, category, DESCRIPTION);

        var name = faker.Person.FirstName;

        account.Balance.ShouldBe(transcation1.Amount - transcation2.Amount);
    }

    [Fact(DisplayName = nameof(AddTransaction_ValidParamter_RetursList))]
    public void AddTransaction_ValidParamter_RetursList()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();

        var transactionFaker = new Faker<Transaction>()
            .CustomInstantiator(f => new Transaction(
                account,
                TransactionType.Income,
                f.Finance.Amount(1),
                f.Date.Past(),
                category,
                f.Commerce.Product()
            ));

        var transaction1 = transactionFaker.Generate();
        var transaction2 = transactionFaker.Generate();

        account.Transactions.ShouldNotBeEmpty();
        account.Transactions.Count.ShouldBe(2);
    }

    [Fact(DisplayName = nameof(AddTransaction_ValidParamter_ReturnsResultBalance))]
    public void AddTransaction_ValidParamter_ReturnsResultBalance()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuilder.Build();

        var transactionFaker = new Faker<Transaction>()
            .CustomInstantiator(f => new Transaction(
                account,
                TransactionType.Income,
                f.Finance.Amount(1),
                f.Date.Past(),
                category,
                f.Commerce.Product()
            ));

        var transaction1 = transactionFaker.Generate();
        var transaction2 = transactionFaker.Generate();

        account.Transactions.Count.ShouldBe(2);
        account.Balance.ShouldBe(transaction1.Amount + transaction2.Amount);
    }

}
