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
        private readonly IRepository<Movie> repository;

        public MovieService(IRepository<Movie> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> AllMovies<T>()
        {
            IQueryable<Movie> movies = this.repository.All();

            return movies.To<T>().ToList();
        }

        public async Task CreateMovie(MovieInputModel inputModel)
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
            };

            await this.repository.AddAsync(movie);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var movie = await this.FindByIdAsync(id);
            this.repository.Delete(movie);
            await this.repository.SaveChangesAsync();
        }

        public async Task<Movie> FindByIdAsync(int? id)
        {
            var movie = await this.repository.All().FirstOrDefaultAsync(x => x.Id == id);
            return movie;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = this.repository.All();
            return movies;
        }

        public T GetById<T>(int id)
        {
            var movie = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return movie;
        }

        public bool MovieExist(int id)
        {
            return this.repository.All().Any(x=>x.Id == id);
        }

        public IEnumerable<T> Upcoming<T>()
        {
            IQueryable<Movie> movies = this.repository.All().Where(x => x.ReleaseDate > DateTime.UtcNow.AddDays(15));

            return movies.To<T>().ToList();
        }

        public async Task UpdateMovie(Movie movie)
        {
            this.repository.Update(movie);
            await this.repository.SaveChangesAsync();
        }
    }
}
