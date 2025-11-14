using Bogus;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using Shouldly;

namespace NaReta.Domain.Test.Entities
{
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
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void Contructor_WhenNameEmptyOrNull_ThrowArgumentException(string name)
        {
            var act = () => new Account(
                string.Empty
            );

            act.ShouldThrow<ArgumentException>(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
        }

        [Fact]
        public void Contructor_WhenTransacionsEmpty_ReturnsBalanceZero()
        {
            var faker = new Faker();
            var name = faker.Person.FirstName;
            var transactions = new List<Transaction>();

            var account = new Account(
                name,
                transactions
            );

            account.ShouldNotBeNull();
            account.Name.ShouldBe(name);
            account.Balance.ShouldBe(0);
        }

        [Fact]
        public void Contructor_WhenTransacionsExists_ReturnsBalanceZero()
        {
            var categoryFaker = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.Name.JobTitle()));

            var transactionFaker = new Faker<Transaction>()
                .CustomInstantiator(f => new Transaction(
                    1,
                    TransactionType.Income,
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    categoryFaker.Generate(),
                    f.Commerce.Product()
                ));

            var transaction1 = transactionFaker.Generate();
            var transaction2 = transactionFaker.Generate();
            List<Transaction> transactions = [transaction1, transaction2];

            var faker = new Faker();
            var name = faker.Person.FirstName;

            var account = new Account(
                name,
                transactions
            );

            account.ShouldNotBeNull();
            account.Name.ShouldBe(name);
            account.Balance.ShouldBe(transaction1.Amount + transaction2.Amount);
        }

        [Fact]
        public void CalculateBalance_WhenTransactionIncome_ReturnsBalancePositive()
        {
            var faker = new Faker();
            TransactionType TYPE = TransactionType.Income;
            decimal amount = faker.Finance.Amount(1);
            DateTime date = faker.Date.Past();
            const string DESCRIPTION = "Receita 1";
            var category = new Category("Salário");

            var transcation1 = new Transaction(1, TYPE, amount, date, category, DESCRIPTION);
            var transcation2 = new Transaction(1, TYPE, amount, date, category, DESCRIPTION);

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.AddTransaction(transcation1);
            account.AddTransaction(transcation2);
            account.Balance.ShouldBe(transcation1.Amount + transcation2.Amount);
        }

        [Fact]
        public void CalculateBalance_WhenTransactionExpense_ReturnsBalanceNegative()
        {
            var faker = new Faker();
            TransactionType TYPE = TransactionType.Expense;
            decimal amount = faker.Finance.Amount(1);
            DateTime date = faker.Date.Past();
            const string DESCRIPTION = "Despesa 1";
            var category = new Category("Salário");

            var transcation1 = new Transaction(1, TYPE, amount, date, category, DESCRIPTION);
            var transcation2 = new Transaction(1, TYPE, amount, date, category, DESCRIPTION);

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.AddTransaction(transcation1);
            account.AddTransaction(transcation2);
            account.Balance.ShouldBe(0 - (transcation1.Amount + transcation2.Amount));
        }

        [Fact]
        public void CalculateBalance_WhenTransactionIncomeGreaterThanExpense_ReturnsBalancePositive()
        {
            var category = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.Name.JobTitle()));

            var faker = new Faker();
            DateTime date = faker.Date.Past();
            const string DESCRIPTION = "Despesa 1";

            var transcation1 = new Transaction(1, TransactionType.Income, faker.Finance.Amount(100, 200), date, category, DESCRIPTION);
            var transcation2 = new Transaction(1, TransactionType.Expense, faker.Finance.Amount(1, 100), date, category, DESCRIPTION);

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.AddTransaction(transcation1);
            account.AddTransaction(transcation2);
            account.Balance.ShouldBe(transcation1.Amount - transcation2.Amount);
        }

        [Fact]
        public void AddTransaction_ValidParamter_RetursList()
        {
            var categoryFaker = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.Name.JobTitle()));

            var transactionFaker = new Faker<Transaction>()
                .CustomInstantiator(f => new Transaction(
                    1,
                    TransactionType.Income,
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    categoryFaker.Generate(),
                    f.Commerce.Product()
                ));

            var transaction1 = transactionFaker.Generate();
            var transaction2 = transactionFaker.Generate();

            var faker = new Faker();
            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.AddTransaction(transaction1);
            account.AddTransaction(transaction2);

            account.Transactions.Count.ShouldBe(2);
        }

        [Fact]
        public void AddTransaction_ValidParamter_ReturnsResultBalance()
        {
            var categoryFaker = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.Name.JobTitle()));

            var transactionFaker = new Faker<Transaction>()
                .CustomInstantiator(f => new Transaction(
                    1,
                    TransactionType.Income,
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    categoryFaker.Generate(),
                    f.Commerce.Product()
                ));

            var transaction1 = transactionFaker.Generate();
            var transaction2 = transactionFaker.Generate();

            var faker = new Faker();
            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.AddTransaction(transaction1);
            account.AddTransaction(transaction2);

            account.Transactions.Count.ShouldBe(2);
            account.Balance.ShouldBe(transaction1.Amount + transaction2.Amount);
        }

    }
}
