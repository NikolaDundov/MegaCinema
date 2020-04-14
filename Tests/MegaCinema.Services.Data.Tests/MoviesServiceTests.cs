namespace MegaCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Web.Areas.Administration.Controllers;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class MoviesServiceTests
    {
        [Fact]
        public async Task MoviesCountShouldReturnMovieInDataBaseUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest").Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            await service.CreateMovie(new MovieInputModel { Id = 1 });
            await service.CreateMovie(new MovieInputModel { Id = 2 });
            await service.CreateMovie(new MovieInputModel { Id = 3 });
            await service.CreateMovie(new MovieInputModel { Id = 4 });
            Assert.Equal(4, service.MoviesCount());
        }

        [Fact]
        public async Task MovieTitleExistsShouldReturnMovieTitleUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest").Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Movies.Add(new Movie { Title = "Test title" });
            await dbContext.SaveChangesAsync();

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            Assert.True(service.MovieTitleExists("Test title"));
        }

        [Fact]
        public async Task MovieIdExistsShouldReturnMovieIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest").Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Movies.Add(new Movie { Id = 1 });
            await dbContext.SaveChangesAsync();

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            Assert.True(service.MovieExist(1));
        }

        [Fact]
        public async Task CreateMovieWithValidInputData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest").Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);

            var inputModel = new MovieInputModel
            {
                Title = "Titanic",
                Actors = "Test actor, test actor, test actor",
                Country = MegaCinema.Data.Models.Enums.Country.USA,
                Description = "test description test description",
                Director = "John West",
                Duration = new System.TimeSpan(1, 35, 33),
                Genre = GenreType.Adventure,
                Language = MegaCinema.Data.Models.Enums.Language.English,
                Poster = "http://testposter.com",
                Rating = MPAARating.G,
                ReleaseDate = new System.DateTime(2020, 2, 15),
                Score = 4.5,
                Trailer = "sometext",
            };
            var id = await service.CreateMovie(inputModel);
            var movie = repository.All().FirstOrDefault(x => x.Id == id);
            Assert.True(movie.Title == "Titanic");
            Assert.True(movie.Director == "John West");
            Assert.True(movie.Score == 4.5);
        }

        [Fact]
        public async Task DeleteMovieShouldRemoveMovieFromRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest12").Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            await service.CreateMovie(new MovieInputModel { Title = "TestTitle", Id = 1, });
            await service.CreateMovie(new MovieInputModel { Title = "Test123", Id = 2, });
            await service.CreateMovie(new MovieInputModel { Title = "AnotherTitle", Id = 12 });
            await service.DeleteById(2);
            await service.DeleteById(1);
            Assert.False(service.MovieTitleExists("Test123"));
            Assert.False(service.MovieTitleExists("estTitle"));
            Assert.True(service.MoviesCount() == 1);
        }

        [Fact]
        public async Task MovieCountShouldReturnCorrectumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesDbTest12").Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
            await service.CreateMovie(new MovieInputModel { Title = "TestTitle" });
            await service.CreateMovie(new MovieInputModel { Title = "Test123" });
            await service.CreateMovie(new MovieInputModel { Id = 12 });
            Assert.True(service.MoviesCount() == 3);
        }

        public List<Movie> TestMoviesData()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Title = "Titaic",
                    Country = MegaCinema.Data.Models.Enums.Country.UK,
                    Rating = MPAARating.G,
                    Duration = new System.TimeSpan(2, 25, 35),
                    Genre = GenreType.Adventure,
                    Language = MegaCinema.Data.Models.Enums.Language.English,
                },
                new Movie
                {
                    Title = "Die hard 5",
                    Language = MegaCinema.Data.Models.Enums.Language.French,
                    Genre = GenreType.Drama,
                    Rating = MPAARating.PG13,
                    Score = 7.5,
                },
            };
        }

        [Fact]
        public async Task AllMoviesMethodShouldReturnCorrectData()
        {
            var repository = new Mock<IRepository<Movie>>();
            var movieService = new Mock<IMoviesService>();
            var projectiosRepo = new Mock<IProjectionsService>();

            movieService.Setup(r => r.AllMovies<Movie>()).Returns(new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Titanic",
                    Country = MegaCinema.Data.Models.Enums.Country.UK,
                    Rating = MPAARating.G,
                    Duration = new System.TimeSpan(2, 25, 35),
                    Genre = GenreType.Adventure,
                    Language = MegaCinema.Data.Models.Enums.Language.English,
                },
                new Movie
                {
                    Id = 2,
                    Title = "Die hard 5",
                    Language = MegaCinema.Data.Models.Enums.Language.French,
                    Genre = GenreType.Drama,
                    Rating = MPAARating.PG13,
                    Score = 7.5,
                },
            });

            var moviesController = new MoviesController(movieService.Object, projectiosRepo.Object);
            var result = moviesController.Details(2);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as Task<IActionResult>;
            Assert.IsType<MovieViewModel>(viewResult);
        }
    }
}
