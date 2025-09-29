using NaReta.Domain.Entities;
using Shouldly;

namespace NaReta.Domain.Test.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void Contructor_ValidParameters_CreateCategory()
        {
            string title = "Alimentação";
            var category = new Category(title);
            category.ShouldNotBeNull();
            category.Title.ShouldBe(title);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("      ")]
        public void Contructor_EmptyOrNullType_ThrowArgumentException(string title)
        {
            var act = () => new Category(title);

            act.ShouldThrow<ArgumentException>(ResourceErrorMessages.TITLE_EMPTY_OR_NULL);
        }
    }
}
