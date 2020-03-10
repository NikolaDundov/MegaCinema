namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class HallSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Halls.AnyAsync())
            {
                return;
            }

            //var seatsPyramid = new List<Seat>();

            //for (int row = 1; row <= 12; row++)
            //{
            //    for (char col = 'A'; col < 'M'; col+=2)
            //    {
            //        var seat = new Seat { Id = row.ToString() + col };
            //    }
            //}

            //await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 1", Seats = seatsPyramid });
        }
    }
}
