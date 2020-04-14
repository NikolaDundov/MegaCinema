namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProjectionsServiceTests
    {
        [Fact]
        public async Task CreateProjectionAsyncWithCorrectDataShouldAddProjectionInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest1").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var projectionInput = new ProjectionInputModel
            {
                CinemaId = 1,
                StartTime = new DateTime(2020, 05, 12, 15, 30, 00),
                MovieId = 5,
                HallId = 10,
                Type = ProjectionType._2D,
            };

            await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 15, 30, 00), 5, 10, ProjectionType._2D);
            await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 19, 30, 00), 3, 11, ProjectionType._4DX);

            Assert.Equal(2, projectionsService.ProjectionsCount());
        }

        [Fact]
        public async Task CreateProjectionAndTestIfProjectionExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest2").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var projectionInput = new ProjectionInputModel
            {
                CinemaId = 1,
                StartTime = new DateTime(2020, 05, 12, 15, 30, 00),
                MovieId = 5,
                HallId = 10,
                Type = ProjectionType._2D,
            };

            var firstIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 15, 30, 00), 5, 10, ProjectionType._2D);
            var secondIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 13, 15, 30, 00), 3, 11, ProjectionType._4DX);
            var thirdIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 16, 15, 30, 00), 8, 12, ProjectionType._4DX);

            Assert.True(projectionsService.ProjectionExists(firstIdToCkeck));
            Assert.True(projectionsService.ProjectionExists(secondIdToCkeck));
            Assert.True(projectionsService.ProjectionExists(thirdIdToCkeck));
        }

        [Fact]
        public async Task DeleteProjectionShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest3").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var firstIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 15, 30, 00), 5, 10, ProjectionType._2D);
            var secondIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 13, 15, 30, 00), 3, 11, ProjectionType._4DX);
            var thirdIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 16, 15, 30, 00), 8, 12, ProjectionType._4DX);

            await projectionsService.DeleteById(firstIdToCkeck);
            await projectionsService.DeleteByMovieId(3);

            Assert.False(projectionsService.ProjectionExists(firstIdToCkeck));
            Assert.False(projectionsService.ProjectionExists(secondIdToCkeck));
            Assert.True(projectionsService.ProjectionExists(thirdIdToCkeck));
            Assert.Equal(1, projectionsService.ProjectionsCount());
        }

        [Fact]
        public async Task DeleteProjectionRangeShouldDeleteAllProjections()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest4").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var firstIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 10, 11, 30, 00), 5, 10, ProjectionType._2D);
            var secondIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 13, 15, 30, 00), 3, 11, ProjectionType._4DX);
            var thirdIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 16, 13, 30, 00), 8, 12, ProjectionType._4DX);
            var forthIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 17, 17, 00, 00), 8, 12, ProjectionType._4DX);

            await projectionsService.DeleteProjectionsRange(
                new DateTime(2020, 05, 10),
                new DateTime(2020, 05, 17));

            Assert.False(projectionsService.ProjectionExists(firstIdToCkeck));
            Assert.False(projectionsService.ProjectionExists(secondIdToCkeck));
            Assert.False(projectionsService.ProjectionExists(thirdIdToCkeck));
            Assert.Equal(0, projectionsService.ProjectionsCount());
        }

        [Fact]
        public async Task DeleteProjectionRangeShouldDeletePartOfTheProjections()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest5").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var firstIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 10, 11, 30, 00), 5, 10, ProjectionType._2D);
            var secondIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 13, 15, 30, 00), 3, 11, ProjectionType._4DX);
            var thirdIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 16, 13, 30, 00), 8, 12, ProjectionType._4DX);
            var forthIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 17, 17, 00, 00), 8, 12, ProjectionType._4DX);

            await projectionsService.DeleteProjectionsRange(
                new DateTime(2020, 05, 10),
                new DateTime(2020, 05, 16));

            Assert.False(projectionsService.ProjectionExists(firstIdToCkeck));
            Assert.False(projectionsService.ProjectionExists(secondIdToCkeck));
            Assert.False(projectionsService.ProjectionExists(thirdIdToCkeck));
            Assert.True(projectionsService.ProjectionExists(forthIdToCkeck));
            Assert.Equal(1, projectionsService.ProjectionsCount());
        }

        [Fact]
        public async Task ProjectionByProjectionIdShouldReturCorrectId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectionsTest5").Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository);

            var firstIdToCkeck = await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 10, 11, 30, 00), 5, 10, ProjectionType._2D);
            //var secondIdToCkeck = await projectionsService.
            //    CreateAsync(1, new DateTime(2020, 05, 13, 15, 30, 00), 3, 11, ProjectionType._4DX);
            //var thirdIdToCkeck = await projectionsService.
            //    CreateAsync(1, new DateTime(2020, 05, 16, 13, 30, 00), 8, 12, ProjectionType._4DX);
            //var forthIdToCkeck = await projectionsService.
            //    CreateAsync(1, new DateTime(2020, 05, 17, 17, 00, 00), 8, 12, ProjectionType._4DX);

            var projection = projectionsService.ProjectionByProjectionId<Projection>(firstIdToCkeck);

            Assert.Equal(1, projection.CinemaId);
        }
    }
}
