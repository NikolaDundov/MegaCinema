namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IProjectionsService
    {
        IEnumerable<T> AllProjectionsByCinema<T>(int id);

        IEnumerable<T> AllProjections<T>();

        T ProjectionByProjectionId<T>(int id);

        IEnumerable<T> ProjectionByMovieId<T>(int id);
    }
}
