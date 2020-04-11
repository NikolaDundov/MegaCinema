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

            dbContext.Movies.Add(new Movie());
            dbContext.Movies.Add(new Movie());
            dbContext.Movies.Add(new Movie());
            dbContext.Movies.Add(new Movie());
            await dbContext.SaveChangesAsync();

            var repository = new EfRepository<Movie>(dbContext);
            var service = new MovieService(repository);
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
    }
}
