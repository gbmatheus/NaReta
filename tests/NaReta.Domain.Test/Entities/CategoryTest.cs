using NaReta.Domain.Entities;
using Shouldly;

namespace NaReta.Domain.Test.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void Contructor_ValidParameters_CreateCategory()
        {
            string name = "Alimentação";
            var category = new Category(name);
            category.ShouldNotBeNull();
            category.Name.ShouldBe(name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("      ")]
        public void Contructor_EmptyOrNullType_ThrowArgumentException(string name)
        {
            var act = () => new Category(name);

            act.ShouldThrow<ArgumentException>(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
        }
    }
}
