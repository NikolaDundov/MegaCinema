namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaMovieSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.CinemaMovies.AnyAsync())
            {
                return;
            }

            var cinemaIds = dbContext.Cinemas.Select(x => x.Id).ToList();
            var moviesIds = dbContext.Movies.Select(x => x.Id).ToList();
            var cinemaMoviesList = new List<CinemaMovies>();

            foreach (var cinameId in cinemaIds)
            {
                foreach (var movieId in moviesIds)
                {
                    var cinemaMovie = new CinemaMovies
                    {
                        CinemaId = cinameId,
                        MovieId = movieId,
                    };
                    cinemaMoviesList.Add(cinemaMovie);
                }
            }

            await dbContext.CinemaMovies.AddRangeAsync(cinemaMoviesList);
        }
    }
}
