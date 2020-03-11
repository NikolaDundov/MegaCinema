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
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Projections.AnyAsync())
            {
                return;
            }

            var now = DateTime.UtcNow;
            var varnaHallsID = dbContext.Halls.Where(x => x.Cinema.City == "Varna").Select(x => x.Id).ToList();
            var moviesId = dbContext.Movies.Select(m => m.Id).ToList();
            var cinemaIdsList = dbContext.Cinemas.Select(c => c.Id).ToList();
            var minutesList = new List<int> { 0, 15, 30, 45 };
            var typesList = new List<int> { 1, 2, 3, 4, };

            for (int cinema = cinemaIdsList.Min(); cinema <= cinemaIdsList.Max(); cinema++)
            {
                for (int month = 3; month <= 6; month++)
                {
                    for (int day = 1; day <= 30; day++)
                    {
                        for (int hour = 9; hour < 22; hour += 3)
                        {
                            var projection = new Projection
                            {
                                CinemaId = cinema,
                                HallId = this.RandomNumberGenerator(varnaHallsID),
                                MovieId = this.RandomNumberGenerator(moviesId),
                                Type = (ProjectionType)this.RandomNumberGenerator(typesList),
                                StartTime = new DateTime(2020, month, day, hour, this.RandomNumberGenerator(minutesList), 0),
                            };

                            await dbContext.Projections.AddAsync(projection);
                        }
                    }
                }
            }
        }

        public int RandomNumberGenerator(List<int> numbers)
        {
            Random random = new Random();
            int randomNumer = numbers[random.Next(0, numbers.Count)];
            return randomNumer;
        }
    }
}
