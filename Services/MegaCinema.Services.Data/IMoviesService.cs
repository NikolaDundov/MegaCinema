namespace MegaCinema.Services.Data
{
    using System.Collections.Generic;

    public interface IMoviesService
    {
        IEnumerable<T> AllMovies<T>();

        T GetById<T>(int id);
    }
}
