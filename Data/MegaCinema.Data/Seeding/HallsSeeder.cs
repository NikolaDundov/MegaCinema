﻿namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class HallsSeeder : ISeeder
    {
        private const char RowNumbers = 'L';
        private const int SeatsPerRow = 16;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Halls.AnyAsync())
            {
                return;
            }

            var cinemaBurgasId = dbContext.Cinemas.Where(x => x.City == "Burgas").Select(x => x.Id).FirstOrDefault();
            for (int i = 1; i <= 5; i++)
            {
                await dbContext.Halls.AddAsync(new Hall
                {
                    CinemaId = cinemaBurgasId,
                    Name = $"Burgas Hall {i}",

                });
            }

            var cinemaVarnaId = dbContext.Cinemas.Where(x => x.City == "Varna").Select(x => x.Id).FirstOrDefault();
            for (int i = 1; i <= 5; i++)
            {
                await dbContext.Halls.AddAsync(new Hall
                {
                    CinemaId = cinemaVarnaId,
                    Name = $"Varna Hall {i}",

                });
            }

            var cinemaPlovdivId = dbContext.Cinemas.Where(x => x.City == "Plovdiv").Select(x => x.Id).FirstOrDefault();
            for (int i = 1; i <= 5; i++)
            {
                await dbContext.Halls.AddAsync(new Hall
                {
                    CinemaId = cinemaPlovdivId,
                    Name = $"Plovdiv Hall {i}",

                });
            }

            var cinemaSofiaId = dbContext.Cinemas.Where(x => x.City == "Sofia").Select(x => x.Id).FirstOrDefault();
            for (int i = 1; i <= 5; i++)
            {
                await dbContext.Halls.AddAsync(new Hall
                {
                    CinemaId = cinemaSofiaId,
                    Name = $"Sofia Hall {i}",

                });
            }
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
    }
}
