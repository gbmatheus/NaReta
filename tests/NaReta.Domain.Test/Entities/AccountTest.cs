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
        public void CalculateBalance_WhenTransactionIncome_ReturnsBalancePositive()
        {
            var faker = new Faker();
            TransactionType TYPE = TransactionType.Income;
            decimal amount = faker.Finance.Amount(1);
            DateTime date = faker.Date.Past();
            const string DESCRIPTION = "Receita 1";
            var category = new Category("Salário");

            var transcation1 = new Transaction(TYPE, amount, date, category, DESCRIPTION);
            var transcation2 = new Transaction(TYPE, amount, date, category, DESCRIPTION);

            List<Transaction> transactions = [transcation1, transcation2];

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.CalculateBalance(transactions);
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

            var transcation1 = new Transaction(TYPE, amount, date, category, DESCRIPTION);
            var transcation2 = new Transaction(TYPE, amount, date, category, DESCRIPTION);

            List<Transaction> transactions = [transcation1, transcation2];

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.CalculateBalance(transactions);
            account.Balance.ShouldBe(0 - (transcation1.Amount + transcation2.Amount));
        }

        [Fact]
        public void CalculateBalance_WhenManyTransaction_ReturnsBalancePositive()
        {
            var faker = new Faker();
            DateTime date = faker.Date.Past();
            const string DESCRIPTION = "Despesa 1";
            var category = new Category("Salário");

            var transcation1 = new Transaction(TransactionType.Income, faker.Finance.Amount(100, 200), date, category, DESCRIPTION);
            var transcation2 = new Transaction(TransactionType.Expense, faker.Finance.Amount(1, 100), date, category, DESCRIPTION);

            List<Transaction> transactions = [transcation1, transcation2];

            var name = faker.Person.FirstName;
            var account = new Account(
                name
            );

            account.CalculateBalance(transactions);
            account.Balance.ShouldBe(transcation1.Amount - transcation2.Amount);
        }
    }
}
