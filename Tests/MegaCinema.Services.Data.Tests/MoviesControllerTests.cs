namespace MegaCinema.Services.Data.Tests
{
    using MegaCinema.Data.Models;
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class MoviesControllerTests
    {
        [Fact]
        public void TestViewModelForAvailableMovies()
        {
            var mockService = new Mock<IMoviesService>();
            mockService.Setup(x => x.AllMovies<MovieViewModel>()).Returns(this.TestMoviesData());

            var controller = new MoviesController(mockService.Object);
            var result = controller.Available();
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<AllMovieViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as AllMovieViewModel;
            Assert.Equal(2, viewModel.AllMovies.Count);
        }

        [Fact]
        public void TestViewModelForUpcomingMovies()
        {
            var mockService = new Mock<IMoviesService>();
            mockService.Setup(x => x.AllMovies<MovieViewModel>()).Returns(this.TestMoviesData());

            var controller = new MoviesController(mockService.Object);
            var result = controller.Upcoming();
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<AllMovieViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as AllMovieViewModel;
            Assert.Equal(1, viewModel.AllMovies.Count);
        }

        public List<MovieViewModel> TestMoviesData()
        {
            return new List<MovieViewModel>
            {
                new MovieViewModel
                {
                    Id = 1,
                    Title = "Titaic",
                    Country = MegaCinema.Data.Models.Enums.Country.UK,
                    Rating = MPAARating.G,
                    Duration = new System.TimeSpan(2, 25, 35),
                    Genre = GenreType.Adventure,
                    Language = MegaCinema.Data.Models.Enums.Language.English,
                    ReleaseDate = new DateTime(2020, 05, 25),
                },
                new MovieViewModel
                {
                    Id = 5,
                    Title = "Die hard 5",
                    Language = MegaCinema.Data.Models.Enums.Language.French,
                    Genre = GenreType.Drama,
                    Rating = MPAARating.PG13,
                    Score = 7.5,
                    ReleaseDate = new DateTime(2020, 02, 11),
                },
                new MovieViewModel
                {
                    Id = 8,
                    Title = "Test movie",
                    Language = MegaCinema.Data.Models.Enums.Language.English,
                    Genre = GenreType.Action,
                    Rating = MPAARating.PG13,
                    Score = 5.3,
                    ReleaseDate = new DateTime(2020, 01, 15),
                },
            };
        }

        public MovieViewModel testMovieData()
        {
            return new MovieViewModel
            {
                Id = 2,
                Title = "Home alone",
                Language = MegaCinema.Data.Models.Enums.Language.English,
                Genre = GenreType.Comedy,
                Rating = MPAARating.PG13,
                Score = 7.5,
                ReleaseDate = new DateTime(2020, 02, 11),
            };
        }



        private MovieInputModel MovieInputModelTest()
        {
            return new MovieInputModel
            {
                Actors = "actors test model",
                Country = MegaCinema.Data.Models.Enums.Country.UK,
                Description = "test description",
                Director = "test director",
                Duration = new TimeSpan(1, 55, 00),
                Genre = GenreType.Action,
                Language = MegaCinema.Data.Models.Enums.Language.Bulgarian,
                Poster = "posterLink",
                Rating = MPAARating.G,
                ReleaseDate = new DateTime(2020, 03, 05),
                Score = 7.0,
                Title = "Title test",
                Trailer = "someTrailer",
            };
        }
    }
}
