namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemasSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Cinemas.AnyAsync())
            {
                return;
            }

            var cinemaVarna = new Cinema
            {
                City = "Varna",
                Address = "bul. Vladislav Varnenchik 186",
                OpenHour = new DateTime(2020, 01, 01, 10, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Varna")).ToList(),
                ImageUrl = "https://i.imgur.com/AJg7KhA.jpg",
            };

            var cinemaBurgas = new Cinema
            {
                City = "Burgas",
                Address = "bul. Yanko Komitov 6",
                OpenHour = new DateTime(2020, 01, 01, 10, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Burgas")).ToList(),
                ImageUrl = "https://i.imgur.com/z8LoyX5.jpg",
            };

            var cinemaSofia = new Cinema
            {
                City = "Sofia",
                Address = "bul. Cherni vrah 100",
                OpenHour = new DateTime(2020, 01, 01, 10, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 30, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Sofia")).ToList(),
                ImageUrl = "https://i.imgur.com/Eb9luDf.jpg",
            };

            var cinemaPlovdiv = new Cinema
            {
                City = "Plovdiv",
                Address = "bul. Ruski 5",
                OpenHour = new DateTime(2020, 01, 01, 9, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Plovdiv")).ToList(),
                ImageUrl = "https://i.imgur.com/JIIWUqG.jpg",
            };

            await dbContext.Cinemas.AddAsync(cinemaVarna);
            await dbContext.Cinemas.AddAsync(cinemaBurgas);
            await dbContext.Cinemas.AddAsync(cinemaSofia);
            await dbContext.Cinemas.AddAsync(cinemaPlovdiv);
        }
    }
}
