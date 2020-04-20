namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels;
    using MegaCinema.Web.ViewModels.Ticket;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TicketsServiceTests
    {
        private ApplicationUser userForTest()
        {
            return new ApplicationUser
            {
                Name = "Gosho",
            };
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
                Movie = new Movie(),
                Seats = new HashSet<Seat>(),
                Tickets = new HashSet<Ticket>(),
                Type = ProjectionType._2D,
                StartTime = new DateTime(2020, 5, 20, 15, 30, 00),
            };
        }

        private Ticket TicketTest()
        {
            return new Ticket
            {
                Movie = this.MovieTest(),
                Price = 10,
                Projection = this.ProjectionTest(),
                Row = 'F',
                SeatNumer = 10,
                Type = TicketType.Adult,
                User = this.userForTest(),
            };
        }

        [Fact]
        public async Task CreateTicketShouldReturCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TicketTest1").Options;

            var dbContext = new ApplicationDbContext(options);

            var ticketsRepository = new EfRepository<Ticket>(dbContext);
            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var ciemasRepository = new EfRepository<Cinema>(dbContext);

            var service = new TicketsService(
                ticketsRepository,
                projectionsRepository,
                hallsRepository,
                seatsRepository,
                moviesRepository,
                ciemasRepository);

            await service.AddTicketAndSeat(1000, "testUserId", 'B', 6, 8.50m);
            Assert.Equal(1, service.TicketsCount());
        }

        [Fact]
        public void GetTicketDetailsShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TicketTest1").Options;

            var dbContext = new ApplicationDbContext(options);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var ticketsRepository = new EfRepository<Ticket>(dbContext);
            var projectionsRepository = new EfRepository<Projection>(dbContext);
            var hallsRepository = new EfRepository<Hall>(dbContext);
            var seatsRepository = new EfRepository<Seat>(dbContext);
            var moviesRepository = new EfRepository<Movie>(dbContext);
            var ciemasRepository = new EfRepository<Cinema>(dbContext);

            var service = new TicketsService(
                ticketsRepository,
                projectionsRepository,
                hallsRepository,
                seatsRepository,
                moviesRepository,
                ciemasRepository);

            ticketsRepository.AddAsync(this.TicketTest()).GetAwaiter().GetResult();
            ticketsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var ticketViewModel = service.GetTicketDetails(1);
            Assert.IsType<TicketViewModel>(ticketViewModel);
            Assert.Equal(0, ticketViewModel.OccupiedSeats.Count);
            Assert.Equal(10, ticketViewModel.Price);
        }
    }
}
