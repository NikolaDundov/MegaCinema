namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class HallsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Halls.AnyAsync())
            {
                return;
            }

            var seatsPyramid = new List<Seat>();
            var seatsSqaure = new List<Seat>();
            int seatNumberFirstRowPyramid = 12;
            int seatNumberFirstRowSquare = 15;

            for (char row = 'A'; row <= 'L'; row++)
            {
                for (int seatNumber = 1; seatNumber <= seatNumberFirstRowPyramid; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seatsPyramid.Add(seat);
                }

                seatNumberFirstRowPyramid += 2;
            }

            for (char row = 'A'; row <= 'M'; row++)
            {
                for (int seatNumber = 1; seatNumber <= seatNumberFirstRowSquare; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seatsSqaure.Add(seat);
                }
            }

            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 1", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 2", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 3", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 4", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 5", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 6", Seats = seatsPyramid });

            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 1", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 2", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 3", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 4", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 5", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 6", Seats = seatsPyramid });

            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 1", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 2", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 3", Seats = seatsSqaure });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 4", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 5", Seats = seatsPyramid });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 6", Seats = seatsPyramid });
        }
    }
}
