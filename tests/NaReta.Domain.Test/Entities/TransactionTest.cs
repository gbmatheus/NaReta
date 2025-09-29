using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using Shouldly;

namespace NaReta.Domain.Test.Entities
{
    public class TransactionTest
    {
        [Fact]
        public void Constructor_ValidParameters_CreateTransaction()
        {
            // Arrange
            TransactionTypeEnum TYPE = TransactionTypeEnum.Expense;
            const decimal AMOUNT = 100.02m;
            DateTime DATE = new DateTime(2025, 1, 1);
            const string DESCRIPTION = "despesa 1";
            var category = new Category("Alimentação");

            // Act
            var transcation = new Transaction(TYPE, AMOUNT, DATE, category, DESCRIPTION);

            // Assert
            transcation.ShouldNotBeNull();
            transcation.Type.ShouldBe(TYPE);
            transcation.Amount.ShouldBe(AMOUNT);
            transcation.Date.ShouldBe(DATE);
            transcation.Description.ShouldBe(DESCRIPTION);
        }

        [Fact]
        public void Constructor_EmptyOrNullType_ThrowArgumentException()
        {
            // Arrange
            const decimal AMOUNT = 100.02m;
            DateTime DATE = new DateTime(2025, 1, 1);
            const string DESCRIPTION = "despesa 1";
            var category = new Category("Alimentação");

            // Act
            var act = () => new Transaction((TransactionTypeEnum) 3, AMOUNT, DATE, category, DESCRIPTION);

            // Assert
            act.ShouldThrow<ArgumentException>(ResourceErrorMessages.TYPE_INVALID);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-19232)]
        public void Constructor_AmountEqualOrLessThanZero_ThrowArgumentException(Decimal amount)
        {
            // Arrange
            TransactionTypeEnum TYPE = TransactionTypeEnum.Expense;
            DateTime DATE = new DateTime(2025, 1, 1);
            const string DESCRIPTION = "despesa 1";
            var category = new Category("Alimentação");

            // Act
            var act = () => new Transaction(TYPE, amount, DATE, category, DESCRIPTION);

            // Assert
            act.ShouldThrow<ArgumentException>(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);
        }

    }
}
