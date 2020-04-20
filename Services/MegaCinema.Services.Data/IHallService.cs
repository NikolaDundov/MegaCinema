namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IHallService
    {
        IEnumerable<T> GetAll<T>();
    }
}
