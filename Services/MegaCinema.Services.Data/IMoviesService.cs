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

        Task<int> CreateMovie(MovieInputModel inputModel);

        IEnumerable<IndexMovieViewModel> GetAllMovies();

        Task<MovieInputModel> FindByIdAsync(int? id);

        Task DeleteById(int id);

        Task UpdateMovie(MovieInputModel movie);

        bool MovieExist(int id);
    }
}
