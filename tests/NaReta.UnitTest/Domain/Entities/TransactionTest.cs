using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;
using NaReta.Domain.Test.Builder;
using Shouldly;

namespace NaReta.UnitTest.Domain.Entities;

public class TransactionTest
{
    [Fact(DisplayName = nameof(Constructor_ValidParameters_CreateTransaction))]
    [Trait("Domain", "Entity - Transaction")]
    public void Constructor_ValidParameters_CreateTransaction()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        // Arrange
        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        // Act
        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        // Assert
        transcation.ShouldNotBeNull();
        transcation.Type.ShouldBe(TYPE);
        transcation.Amount.ShouldBe(AMOUNT);
        transcation.Date.ShouldBe(DATE);
        transcation.Description.ShouldBe(DESCRIPTION);
    }

    [Fact(DisplayName = nameof(Constructor_EmptyOrNullType_ThrowDomainException))]
    [Trait("Domain", "Entity - Transaction")]
    public void Constructor_EmptyOrNullType_ThrowDomainException()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        // Arrange
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        // Act
        var act = () => new Transaction(account, (TransactionType)3, AMOUNT, DATE, category, DESCRIPTION);

        // Assert
        act.ShouldThrow<DomainException>(ResourceErrorMessages.TYPE_INVALID);
    }

    [Theory(DisplayName = nameof(Constructor_AmountEqualOrLessThanZero_ThrowDomainException))]
    [Trait("Domain", "Entity - Transaction")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-19232)]
    public void Constructor_AmountEqualOrLessThanZero_ThrowDomainException(Decimal amount)
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        // Arrange
        TransactionType TYPE = TransactionType.Expense;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        // Act
        var act = () => new Transaction(account, TYPE, amount, DATE, category, DESCRIPTION);

        // Assert
        act.ShouldThrow<DomainException>(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);
    }

    [Fact(DisplayName = nameof(ChangeType_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeType_ValidParameters_ChangedSuccessfully()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        transcation.ChangeType(TransactionType.Income);
        transcation.Type.ShouldBe(TransactionType.Income);
    }

    [Fact(DisplayName = nameof(ChangeAmount_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeAmount_ValidParameters_ChangedSuccessfully()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        transcation.ChangeAmount(200.01m);
        transcation.Amount.ShouldBe(200.01m);
    }

    [Fact(DisplayName = nameof(ChangeAmount_WhenAmountNegative_ThrowDomainException))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeAmount_WhenAmountNegative_ThrowDomainException()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        var act = () => transcation.ChangeAmount(-10m);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);
    }

    [Fact(DisplayName = nameof(ChangeDate_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeDate_ValidParameters_ChangedSuccessfully()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        transcation.ChangeDate(DateTime.Today);
        transcation.Date.ShouldBe(DateTime.Today);
    }

    [Fact(DisplayName = nameof(ChangeDescription_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeDescription_ValidParameters_ChangedSuccessfully()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        transcation.ChangeDescription("Despesa 2");
        transcation.Description.ShouldBe("Despesa 2");
    }

    [Fact(DisplayName = nameof(ChangeCategory_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Transaction")]
    public void ChangeCategory_ValidParameters_ChangedSuccessfully()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);
        var newCategory = new Category("Alimentação");

        transcation.ChangeCategory(newCategory);
        transcation.Category.ShouldBe(newCategory);
    }

    [Fact(DisplayName = nameof(ApplyChanges_ValidParameter_ChangedAttributeTransaciont))]
    [Trait("Domain", "Entity - Transaction")]
    public void ApplyChanges_ValidParameter_ChangedAttributeTransaciont()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        TransactionType newType = TransactionType.Income;
        const decimal NEW_AMOUNT = 200.02m;
        DateTime newDate = DateTime.Today;
        const string NEW_DESCRIPTION = "nova despesa";
        var newCategory = new Category("Transporte");

        transcation.ApplyChanges(newType, NEW_AMOUNT, newDate, newCategory, NEW_DESCRIPTION);

        transcation.Type.ShouldBe(newType);
        transcation.Category.ShouldBe(newCategory);
        transcation.Amount.ShouldBe(NEW_AMOUNT);
        transcation.Date.ShouldBe(newDate);
        transcation.Description.ShouldBe(NEW_DESCRIPTION);
    }

    [Fact(DisplayName = nameof(ApplyChanges_WhenTypeInvalid_ThrowDomainException))]
    [Trait("Domain", "Entity - Transaction")]
    public void ApplyChanges_WhenTypeInvalid_ThrowDomainException()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        const decimal NEW_AMOUNT = 200.02m;
        DateTime newDate = DateTime.Today;
        const string NEW_DESCRIPTION = "nova despesa";
        var newCategory = CategoryEntityBuilder.Build();

        var act = () => transcation.ApplyChanges((TransactionType)3, NEW_AMOUNT, newDate, newCategory, NEW_DESCRIPTION);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.TYPE_INVALID);
    }

    [Fact]
    [Fact(DisplayName = nameof(Constructor_ValidParameters_CreateTransaction))]
    [Trait("Domain", "Entity - Transaction")]
    public void ApplyChanges_WhenAmountNegative_ThrowDomainException()
    {
        var account = AccountEntityBuildes.Build();
        var category = CategoryEntityBuilder.Build();

        TransactionType TYPE = TransactionType.Expense;
        const decimal AMOUNT = 100.02m;
        DateTime DATE = new DateTime(2025, 1, 1);
        const string DESCRIPTION = "despesa 1";

        var transcation = new Transaction(account, TYPE, AMOUNT, DATE, category, DESCRIPTION);

        TransactionType newType = TransactionType.Income;
        const decimal NEW_AMOUNT = -1m;
        DateTime newDate = DateTime.Today;
        const string NEW_DESCRIPTION = "nova despesa";
        var newCategory = CategoryEntityBuilder.Build();

        var act = () => transcation.ApplyChanges(newType, NEW_AMOUNT, newDate, newCategory, NEW_DESCRIPTION);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.TYPE_INVALID);
    }
}
