namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Globalization;

    using MegaCinema.Web.Infrastructure.CustomAttributes;
    using MegaCinema.Web.ViewModels.Projection;
    using Xunit;

    public class TitleValidationAttribiteTests
    {
        [Fact]
        public void MovieTitleShouldBevalid()
        {
            // Arrange
            var validationAttribute = new TitleValidationAttribite();

            // Act
            var isValid = validationAttribute.IsValid("Valid Title");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void MovieTitleShouldNotBevalid()
        {
            // Arrange
            var validationAttribute = new TitleValidationAttribite();

            // Act
            var isValid = validationAttribute.IsValid("Invaid title*");

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Movie 123,.-", true)]
        [InlineData("TiTlE .,-09", true)]
        [InlineData("Top Gun 3", true)]
        [InlineData("Fred's", true)]
        [InlineData("Die Hard 4.0", true)]
        [InlineData("1916 & ", true)]
        [InlineData("You & Me", true)]
        [InlineData("911 -", true)]
        public void ValidMovieTitles(string title, bool expectedResult)
        {
            var validationAttribute = new TitleValidationAttribite();
            var actualResult = validationAttribute.IsValid(title);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("Movie *>", false)]
        [InlineData("Title movie >", false)]
        [InlineData("Top Gun <", false)]
        [InlineData("Jojo {", false)]
        [InlineData("Die Hard ~", false)]
        [InlineData("invalid | ", false)]
        [InlineData("invalid //", false)]
        [InlineData("test//\\", false)]
        public void InvalidMovieTitles(string title, bool expectedResult)
        {
            var validationAttribute = new TitleValidationAttribite();
            var actualResult = validationAttribute.IsValid(title);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
