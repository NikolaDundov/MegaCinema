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

            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 1", Seats = CreateRectangleSeatsHall('M', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 2", Seats = CreateRectangleSeatsHall('L', 11) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 3", Seats = CreateRectangleSeatsHall('L', 10) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 4", Seats = CreatePyramidSeatsHall('L', 11) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 5", Seats = CreatePyramidSeatsHall('L', 11) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Burgas 6", Seats = CreatePyramidSeatsHall('L', 11) });

            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 1", Seats = CreateRectangleSeatsHall('N', 13) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 2", Seats = CreateRectangleSeatsHall('M', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 3", Seats = CreateRectangleSeatsHall('M', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 4", Seats = CreatePyramidSeatsHall('L', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 5", Seats = CreatePyramidSeatsHall('L', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Varna 6", Seats = CreatePyramidSeatsHall('L', 12) });

            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 1", Seats = CreateRectangleSeatsHall('M', 14) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 2", Seats = CreateRectangleSeatsHall('N', 14) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 3", Seats = CreateRectangleSeatsHall('N', 14) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 4", Seats = CreatePyramidSeatsHall('M', 13) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 5", Seats = CreatePyramidSeatsHall('M', 13) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Plovdiv 6", Seats = CreatePyramidSeatsHall('N', 15) });

            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 1", Seats = CreateRectangleSeatsHall('O', 14) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 2", Seats = CreateRectangleSeatsHall('N', 13) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 3", Seats = CreateRectangleSeatsHall('N', 13) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 4", Seats = CreatePyramidSeatsHall('M', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 5", Seats = CreatePyramidSeatsHall('M', 12) });
            await dbContext.Halls.AddAsync(new Hall { Name = "Sofia 6", Seats = CreatePyramidSeatsHall('M', 12) });
        }

        private static List<Seat> CreateRectangleSeatsHall(char lastRow, int firstRowSeatsCount)
        {
            var seats = new List<Seat>();
            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= firstRowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }

        private static List<Seat> CreatePyramidSeatsHall(char lastRow, int firstRowSeatsCount)
        {
            var seats = new List<Seat>();

            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= firstRowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }

                firstRowSeatsCount += 2;
            }

            return seats;
        }
    }
}
