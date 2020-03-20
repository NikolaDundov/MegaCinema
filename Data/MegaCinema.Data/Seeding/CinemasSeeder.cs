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
                OpenHour = new DateTime(2020, 01, 01, 9, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Varna")).ToList(),
                ImageUrl = "https://www.domaza.bg/upload/articles/15/299915/fdcf2f18e96494a4febde9370545f956.jpg",
            };

            var cinemaBurgas = new Cinema
            {
                City = "Burgas",
                Address = "bul. Yanko Komitov 6",
                OpenHour = new DateTime(2020, 01, 01, 9, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Burgas")).ToList(),
                ImageUrl = "https://imgrabo.com/pics/guide/900x600/20160704180330_40591.jpg",
            };

            var cinemaSofia = new Cinema
            {
                City = "Sofia",
                Address = "bul. Cherni vrah 100",
                OpenHour = new DateTime(2020, 01, 01, 9, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 30, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Sofia")).ToList(),
                ImageUrl = "https://www.property-forum.eu/feltoltesek/cikkek/1185/1500/paradise_centre-601.jpg",
            };

            var cinemaPlovdiv = new Cinema
            {
                City = "Plovdiv",
                Address = "bul. Ruski 5",
                OpenHour = new DateTime(2020, 01, 01, 9, 00, 0),
                ClosingHour = new DateTime(2020, 01, 01, 23, 00, 0),
                Halls = dbContext.Halls.Where(h => h.Name.Contains("Plovdiv")).ToList(),
                ImageUrl = "https://cdn.marica.bg/images/marica.bg/238/640_238456.jpeg",
            };

            await dbContext.Cinemas.AddAsync(cinemaVarna);
            await dbContext.Cinemas.AddAsync(cinemaBurgas);
            await dbContext.Cinemas.AddAsync(cinemaSofia);
            await dbContext.Cinemas.AddAsync(cinemaPlovdiv);
        }
    }
}
