using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Entities;
using Shouldly;

namespace NaReta.UnitTest.Domain.Entities;

public class CategoryTest
{
    [Fact(DisplayName = nameof(Contructor_ValidParameters_CreateCategory))]
    [Trait("Domain", "Entity - Category")]
    public void Contructor_ValidParameters_CreateCategory()
    {
        string name = "Alimentação";
        var category = new Category(name);
        category.ShouldNotBeNull();
        category.Name.ShouldBe(name);
    }

    [Theory(DisplayName = nameof(Contructor_EmptyOrNullType_ThrowDomainException))]
    [Trait("Domain", "Entity - Category")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("      ")]
    public void Contructor_EmptyOrNullType_ThrowDomainException(string name)
    {
        var act = () => new Category(name);

        act.ShouldThrow<DomainException>(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
    }

    [Theory(DisplayName = nameof(ChangeName_ValidParameters_ChangedSuccessfully))]
    [Trait("Domain", "Entity - Category")]
    [InlineData("Habitação")]
    [InlineData("Saúde")]
    public void ChangeName_ValidParameters_ChangedSuccessfully(string newName)
    {
        string name = "Alimentação";
        var category = new Category(name);
        category.ChangeName(newName);
        
        category.Name.ShouldBe(newName);
    }
}
