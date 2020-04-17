namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using MegaCinema.Web.ViewModels.CustomAttributes;
    using Xunit;

    public class MovieDurationAttributesTests
    {
        [Fact]
        public void TimeSpanShouldBeValid()
        {
            // Arrange
            var validationAttribute = new MovieDurationAttribute();

            // Act
            var isValid = validationAttribute.IsValid(new TimeSpan(01, 25, 55));

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void TimeSpanShouldNotBeValid()
        {
            // Arrange
            var validationAttribute = new MovieDurationAttribute();

            // Act
            var isValid = validationAttribute.IsValid(new TimeSpan(00, 19, 00));

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("00:21:00", true)]
        [InlineData("00:59:00", true)]
        [InlineData("01:59:00", true)]
        [InlineData("02:59:00", true)]
        [InlineData("03:59:00", true)]
        [InlineData("04:59:00", true)]
        public void TimeSpanListShouldBeValid(string timeSpan, bool expectedResult)
        {
            var validationAttribute = new MovieDurationAttribute();
            string formatTime = "hh\\:mm\\:ss";

            var actualResult = validationAttribute.IsValid(TimeSpan.ParseExact(timeSpan, formatTime, CultureInfo.InvariantCulture));

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("00:19:59", false)]
        [InlineData("05:01:00", false)]
        [InlineData("05:00:00", false)]
        [InlineData("00:00:00", false)]
        [InlineData("05:59:00", false)]
        [InlineData("07:00:00", false)]
        public void TimeSpanListShouldNotBeValid(string timeSpan, bool expectedResult)
        {
            var validationAttribute = new MovieDurationAttribute();
            string formatTime = "hh\\:mm\\:ss";

            var actualResult = validationAttribute.IsValid(TimeSpan.ParseExact(timeSpan, formatTime, CultureInfo.InvariantCulture));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
