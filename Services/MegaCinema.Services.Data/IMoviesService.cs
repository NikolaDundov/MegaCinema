namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IMoviesService
    {
        IEnumerable<T> AllMovies<T>();
    }
}
