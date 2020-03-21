namespace MegaCinema.Services.Data
{
    using System.Collections.Generic;

    public interface IMoviesService
    {
        IEnumerable<T> AllMovies<T>();

        IEnumerable<T> Upcoming<T>();

        T GetById<T>(int id);
    }
}
