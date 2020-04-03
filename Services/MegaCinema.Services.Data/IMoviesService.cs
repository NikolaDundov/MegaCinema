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

        Task<T> GetByIdAsync<T>(int? id);

        Task<int> CreateMovie(MovieInputModel inputModel);

        Task DeleteById(int id);

        Task UpdateMovie(MovieInputModel movie);

        bool MovieExist(int id);

        bool MovieTitleExists(string title);

        int MoviesCount();
    }
}
