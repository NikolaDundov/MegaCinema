namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProjectionsSeeder : ISeeder
    {
        private const int FirstProjectionTime = 10;
        private const int LastProjectionTime = 22;
        private const int ProjectionIntervalTime = 3;


        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Projections.AnyAsync())
            {
                return;
            }

            var cinemaCities = dbContext.Cinemas.Select(x => x.City).ToList();

            foreach (var city in cinemaCities)
            {
                await dbContext.Projections.AddRangeAsync(GenerateProjectionsInCinema(dbContext, city));
            }
        }

        private static int RandomNumberGenerator(List<int> numbers)
        {
            Random random = new Random();
            int randomNumer = numbers[random.Next(0, numbers.Count)];
            return randomNumer;
        }

        private static List<Projection> GenerateProjectionsInCinema(ApplicationDbContext dbContext, string city)
        {
            var projections = new List<Projection>();
            var hallsIdList = dbContext.Halls.Where(x => x.Cinema.City == city).Select(x => x.Id).ToList();
            var moviesIdList = dbContext.Movies.Select(m => m.Id).ToList();
            var cinemaId = dbContext.Cinemas.Where(c => c.City == city).Select(c => c.Id).FirstOrDefault();
            var minutesList = new List<int> { 0, 15, 30, 45 };
            var typesList = new List<int> { 1, 2, 3, 4, };

            for (int month = 4; month <= 4; month++)
            {
                int totalDays = DateTime.DaysInMonth(2020, month);
                for (int day = 1; day <= totalDays; day++)
                {
                    for (int hour = FirstProjectionTime; hour < LastProjectionTime; hour += ProjectionIntervalTime)
                    {
                        var projection = new Projection
                        {
                            CinemaId = cinemaId,
                            HallId = RandomNumberGenerator(hallsIdList),
                            MovieId = RandomNumberGenerator(moviesIdList),
                            Type = (ProjectionType)RandomNumberGenerator(typesList),
                            StartTime = new DateTime(2020, month, day, hour, RandomNumberGenerator(minutesList), 0),
                            Seats = CreateRectangleSeatsHall('L', 16),
                        };
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }

        private static List<Seat> CreateRectangleSeatsHall(char lastRow, int rowSeatsCount)
        {
            var seats = new List<Seat>();
            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= rowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }
}
