namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;

    public class MovieService : IMoviesService
    {
        private readonly IRepository<Movie> movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public IEnumerable<T> AllMovies<T>()
        {
            IQueryable<Movie> movies = this.movieRepository.All();

            return movies.To<T>().ToList();
        }

        public async Task<int> CreateMovie(MovieInputModel inputModel)
        {
            var movie = new Movie
            {
                Title = inputModel.Title,
                Actors = inputModel.Actors,
                Description = inputModel.Description,
                Country = inputModel.Country,
                Director = inputModel.Director,
                Language = inputModel.Language,
                Poster = inputModel.Poster,
                Genre = inputModel.Genre,
                Rating = inputModel.Rating,
                Score = inputModel.Score,
                Trailer = inputModel.Trailer,
                Duration = inputModel.Duration,
                ReleaseDate = inputModel.ReleaseDate,
            };

            await this.movieRepository.AddAsync(movie);
            await this.movieRepository.SaveChangesAsync();
            return movie.Id;
        }

        public async Task DeleteById(int id)
        {
            var movie = await this.movieRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.movieRepository.Delete(movie);
            await this.movieRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int? id)
        {
            var movie = await this.movieRepository.All()
                .Where(x => x.Id == id).To<T>()
                .FirstOrDefaultAsync();

            return movie;
        }

        public bool MovieExist(int id)
        {
            return this.movieRepository.All().Any(x => x.Id == id);
        }

        public IEnumerable<T> Upcoming<T>()
        {
            IQueryable<Movie> movies = this.movieRepository.All().Where(x => x.ReleaseDate > DateTime.UtcNow.AddDays(15));

            return movies.To<T>().ToList();
        }

        public async Task UpdateMovie(MovieInputModel movie)
        {
            var movieToUpdate = AutoMapperConfig.MapperInstance.Map<Movie>(movie);
            this.movieRepository.Update(movieToUpdate);
            await this.movieRepository.SaveChangesAsync();
        }
    }
}
