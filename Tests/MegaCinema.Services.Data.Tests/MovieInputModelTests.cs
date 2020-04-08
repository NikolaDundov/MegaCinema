namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Web.ViewModels.Movie;
    using Xunit;

    public class MovieInputModelTests
    {
        [Fact]
        public void InputModelShouldBeValid()
        {
            var viewModel = new MovieInputModel
            {
                Title = "Jojo",
                Genre = GenreType.Comedy,
                Country = Country.USA,
                Description = "A World War II satire that follows a lonely German " +
                "boy named Jojo (Roman Griffin Davis) whose world view is turned upside " +
                "down when he discovers his single mother (Scarlett Johansson) is hiding a " +
                "young Jewish girl (Thomasin McKenzie) in their attic. Aided only by his " +
                "idiotic imaginary friend, Adolf Hitler (Taika Waititi), Jojo must confront " +
                "his blind nationalism.",
                Director = "Taika Waititi",
                Actors = "Roman Griffin Davis, Thomasin McKenzie, Scarlett Johansson, Taika Waititi",
                Duration = new TimeSpan(1, 45, 52),
                Poster = "https://i.imgur.com/F7gxP9z.jpg",
                Language = Language.English,
                Rating = MPAARating.G,
                Trailer = "tL4McUzXfFI",
                ReleaseDate = new DateTime(2020, 01, 15),
                Score = 7.1,
            };

            //viewModel.Validate();
        }
    }
}
