namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Web.Areas.Administration.Controllers;
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class MoviesControllerTests
    {
        [Fact]
        public void TestViewModelForAvailableMovies()
        {
            var mockService = new Mock<IMoviesService>();
            mockService.Setup(x => x.AllMovies<MovieViewModel>()).Returns(this.TestMoviesData());

            var controller = new Web.Controllers.MoviesController(mockService.Object);
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

            var controller = new Web.Controllers.MoviesController(mockService.Object);
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

        [Fact]
        public async Task TestCreateMovieShouldAddMovieToRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest135").Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            var result = await service.CreateMovie(this.MovieInputModelTest());

            var movie = await repository.All().FirstOrDefaultAsync(x => x.Id == result);
            Assert.Equal("Title test", movie.Title);
            Assert.Equal(7.0, movie.Score);
            Assert.Equal("actors test model", movie.Actors);
            Assert.Equal(1, result);
            Assert.True(service.MovieExist(1));
            Assert.Equal(1, service.MoviesCount());
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

        private MovieInputModel NewMovieInputModelTest()
        {
            return new MovieInputModel
            {
                Actors = "new test model",
                Country = MegaCinema.Data.Models.Enums.Country.USA,
                Description = "new test description",
                Director = "new director",
                Duration = new TimeSpan(1, 00, 00),
                Genre = GenreType.Crime,
                Language = MegaCinema.Data.Models.Enums.Language.French,
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
