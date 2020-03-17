namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICinemaService
    {
        T ShowProjections<T>(int id);

        IEnumerable<T> AllProjectionByDate<T>(DateTime date);

        IEnumerable<T> AllCinemas<T>();
    }
}
