using Bogus;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using NaReta.Domain.Test.Builder;
using Shouldly;

namespace NaReta.UnitTest.Entities;

public class AccountTest
{
    [Fact]
    public void Contructor_ValidParameters_CreateAccount()
    {
        var faker = new Faker();
        var name = faker.Person.FirstName;

        var account = new Account(
            name
        );

        account.ShouldNotBeNull();
        account.Name.ShouldBe(name);
        account.Balance.ShouldBe(0);
        account.Transactions.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Contructor_WhenNameEmptyOrNull_ThrowDomainException(string name)
    {
        var act = () => new Account(
            name
        );

        act.ShouldThrow<DomainException>(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
    }

    [Fact]
    public void CalculateBalance_WhenTransactionIncome_ReturnsBalancePositive()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuildes.Build();
        
        var faker = new Faker();
        TransactionType TYPE = TransactionType.Income;
        decimal amount = faker.Finance.Amount(1);
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Receita 1";
        
        var transcation1 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);

        account.Balance.ShouldBe(transcation1.Amount + transcation2.Amount);
    }

    [Fact]
    public void CalculateBalance_WhenTransactionExpense_ReturnsBalanceNegative()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuildes.Build();

        var faker = new Faker();
        TransactionType TYPE = TransactionType.Expense;
        decimal amount = faker.Finance.Amount(1);
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Despesa 1";

        var transcation1 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TYPE, amount, date, category, DESCRIPTION);

        account.Balance.ShouldBe(0 - (transcation1.Amount + transcation2.Amount));
    }

    [Fact]
    public void CalculateBalance_WhenTransactionIncomeGreaterThanExpense_ReturnsBalancePositive()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuildes.Build();

        var faker = new Faker();
        DateTime date = faker.Date.Past();
        const string DESCRIPTION = "Despesa 1";

        var transcation1 = new Transaction(account, TransactionType.Income, faker.Finance.Amount(100, 200), date, category, DESCRIPTION);
        var transcation2 = new Transaction(account, TransactionType.Expense, faker.Finance.Amount(1, 100), date, category, DESCRIPTION);

        var name = faker.Person.FirstName;

        account.Balance.ShouldBe(transcation1.Amount - transcation2.Amount);
    }

    [Fact]
    public void AddTransaction_ValidParamter_RetursList()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuildes.Build();

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

    [Fact]
    public void AddTransaction_ValidParamter_ReturnsResultBalance()
    {
        var category = CategoryEntityBuilder.Build();
        var account = AccountEntityBuildes.Build();

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
