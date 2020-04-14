using MegaCinema.Data;
using MegaCinema.Data.Models;
using MegaCinema.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MegaCinema.Services.Data.Tests
{
    public class TicketsServiceTests
    {
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
    }
}
