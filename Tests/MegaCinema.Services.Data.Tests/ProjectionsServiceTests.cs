namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProjectionsServiceTests
    {
        private List<ProjectionViewModel> ProjectionsViewModelForTest()
        {
            return new List<ProjectionViewModel>()
            {
                new ProjectionViewModel { Id = 1, MovieId = 4, MovieTitle = "Test", Type = ProjectionType._2D },
                new ProjectionViewModel { Id = 2, MovieId = 6, MovieTitle = "Test 5", Type = ProjectionType._3D },
            };
        }

        private EfRepository<Projection> RepositoryForTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            return projectionsRepository;
        }

        private ProjectionsService ProjectionServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var cinemaRepository = new EfRepository<Cinema>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);
            var ticketsRepository = new EfRepository<Ticket>(dbContext);

            var projectionsService = new ProjectionsService(
                projectionsRepository,
                moviesRepository,
                cinemaRepository,
                hallsRepository,
                seatsRepository,
                ticketsRepository);

            return projectionsService;
        }

        private Movie MovieTest()
        {
            return new Movie
            {
                Title = "Test",
                Actors = "some actors test",
                Country = MegaCinema.Data.Models.Enums.Country.USA,
                Description = "some description",
                Director = "John Smith",
                Genre = GenreType.Action,
                Language = MegaCinema.Data.Models.Enums.Language.English,
                Poster = "test.",
                Score = 7.1,
                Duration = new TimeSpan(1, 25, 00),
                Rating = MPAARating.G,
                Trailer = "testtrailer",
            };
        }

        private Cinema CinemaTest()
        {
            return new Cinema
            {
                Address = "bul. Vitosha 100",
                City = "Sofia",
                OpenHour = new DateTime(2020, 1, 1, 11, 00, 00),
                ClosingHour = new DateTime(2020, 1, 1, 23, 0, 0),
                Halls = new HashSet<Hall>(),
                Projections = new HashSet<Projection>(),
            };
        }

        private Hall HallForTest()
        {
            return new Hall
            {
                Cinema = this.CinemaTest(),
                CinemaId = 1,
                Name = "Hall name",
            };
        }

        private Projection ProjectionTest()
        {
            return new Projection
            {
                Cinema = this.CinemaTest(),
                Hall = this.HallForTest(),
                Movie = this.MovieTest(),
                Seats = new HashSet<Seat>(),
                Tickets = new HashSet<Ticket>(),
                Type = ProjectionType._2D,
                StartTime = new DateTime(2020, 5, 20, 15, 30, 00),
            };
        }

        [Fact]
        public async Task CreateProjectionAsyncWithCorrectDataShouldAddProjectionInDatabase()
        {
            var projectionsService = this.ProjectionServiceTest();
            await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 15, 30, 00), 5, 10, ProjectionType._2D);
            await projectionsService.
                CreateAsync(1, new DateTime(2020, 05, 12, 19, 30, 00), 3, 11, ProjectionType._4DX);

            Assert.Equal(2, projectionsService.ProjectionsCount());
        }

        [Fact]
        public async Task CreateProjectionAndTestIfProjectionExists()
        {
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

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
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

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
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

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
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

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
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

            await projectionsRepository.AddAsync(this.ProjectionTest());
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var projection = projectionsService
            .ProjectionByProjectionId<ProjectionViewModel>(1);

            Assert.Equal("Test", projection.MovieTitle);
            Assert.Equal(4, projectionsService.ProjectionsCount());
        }

        [Fact]
        public void FindProjectionsByMovieIdShouldReturnCorrectData()
        {
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            projectionsRepository.AddAsync(this.ProjectionTest()).GetAwaiter().GetResult();
            projectionsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var projectionViewModel = projectionsService.ProjectionByMovieId<ProjectionViewModel>(1);
            foreach (var projection in projectionViewModel)
            {
                Assert.Equal(1, projection.MovieId);
            }
        }

        [Fact]
        public void ByMovieIdAdCinemaIdShouldWorkCorrectly()
        {
            var projectionsService = this.ProjectionServiceTest();
            var projectionsRepository = this.RepositoryForTest();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            projectionsRepository.AddAsync(this.ProjectionTest()).GetAwaiter().GetResult();
            projectionsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var projections = projectionsService
                .ProjectionByMovieIdAdCinemaId<ProjectionViewModel>(1, 1, new DateTime(2020, 5, 20, 15, 30, 00));

            foreach (var projection in projections)
            {
                Assert.Equal(1, projection.HallId);
                Assert.Equal(1, projection.MovieId);
                Assert.Equal(ProjectionType._2D, projection.Type);
                Assert.Equal(1, projection.CinemaId);
            }
        }

        [Fact]
        public void ProjectionByMovieIdAndCinemaIdOnlyShouldWorkCorrectly()
        {
            var projectionsService = this.ProjectionServiceTest();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var projectionsRepository = this.RepositoryForTest();
            projectionsRepository.AddAsync(this.ProjectionTest()).GetAwaiter().GetResult();
            projectionsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var projections = projectionsService
                .ProjectionByMovieIdAndCinemaIdOnly<ProjectionViewModel>(1, 1);

            foreach (var projection in projections)
            {
                Assert.Equal(1, projection.HallId);
                Assert.Equal(1, projection.MovieId);
                Assert.Equal(ProjectionType._2D, projection.Type);
                Assert.Equal(1, projection.CinemaId);
            }
        }

        [Fact]
        public void ProjectionsStartTimeShouldWorkCorrectly()
        {
            var projectionsService = this.ProjectionServiceTest();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var projectionsRepository = this.RepositoryForTest();
            projectionsRepository.AddAsync(this.ProjectionTest()).GetAwaiter().GetResult();
            projectionsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var projectionsStartTime = projectionsService.ProjectionsStartTime(1);

            foreach (var startTime in projectionsStartTime)
            {
                Assert.Equal(new DateTime(2020, 5, 20, 15, 30, 00), startTime);
            }
        }
    }
}
