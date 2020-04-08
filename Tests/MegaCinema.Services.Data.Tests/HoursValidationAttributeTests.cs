namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Globalization;

    using MegaCinema.Web.ViewModels.Projection;
    using Xunit;

    public class HoursValidationAttributeTests
    {
        [Fact]
        public void HourShouldBeValid()
        {
            // Arrange
            var validationAttribute = new HoursValidationAttribute();

            // Act
            var isValid = validationAttribute.IsValid(new DateTime(2020, 04, 25, 10, 30, 00));

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void HourShouldNotBeValid()
        {
            // Arrange
            var validationAttribute = new HoursValidationAttribute();

            // Act
            var isValid = validationAttribute.IsValid(new DateTime(2020, 04, 25, 09, 59, 00));

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("2020-05-01 22:29", true)]
        [InlineData("2020-11-09 10:45", true)]
        [InlineData("2020-04-10 10:31", true)]
        [InlineData("2020-04-10 10:30", true)]
        [InlineData("2020-04-09 13:00", true)]
        [InlineData("2020-05-09 22:30", true)]
        [InlineData("2020-05-09 21:00", true)]
        [InlineData("2020-05-09 22:22", true)]
        public void ValidOpenAndClosedHours(string date, bool expectedResult)
        {
            var validationAttribute = new HoursValidationAttribute();
            var actualResult = validationAttribute.IsValid(DateTime.ParseExact(
                date, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("2020-05-01 22:32", false)]
        [InlineData("2020-02-09 09:29", false)]
        [InlineData("2020-04-10 09:28", false)]
        [InlineData("2020-04-09 05:00", false)]
        [InlineData("2020-05-09 22:31", false)]
        [InlineData("2020-05-09 23:00", false)]
        [InlineData("2020-05-09 23:45", false)]
        public void InvalidOpenAndClosedHours(string date, bool expectedResult)
        {
            var validationAttribute = new HoursValidationAttribute();
            var actualResult = validationAttribute.IsValid(DateTime.ParseExact(
                date, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
