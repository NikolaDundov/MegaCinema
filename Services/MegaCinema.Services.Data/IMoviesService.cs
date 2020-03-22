namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Movie;

    public interface IMoviesService
    {
        IEnumerable<T> AllMovies<T>();

        IEnumerable<T> Upcoming<T>();

        T GetById<T>(int id);

        Task CreateMovie(MovieInputModel inputModel);

        IEnumerable<Movie> GetAllMovies();

        Task<Movie> FindByIdAsync(int? id);

        Task DeleteById(int id);

        Task UpdateMovie(Movie movie);

        bool MovieExist(int id);
    }
}
