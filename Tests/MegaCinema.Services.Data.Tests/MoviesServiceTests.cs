namespace MegaCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
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
    }
}
