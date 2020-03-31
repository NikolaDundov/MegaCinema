namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Projection;

    public interface IProjectionsService
    {
        IEnumerable<T> AllProjectionsByCinema<T>(string cinemaName);

        IEnumerable<T> AllProjections<T>();

        T ProjectionByProjectionId<T>(int? id);

        IEnumerable<T> ProjectionByMovieId<T>(int id);

        Task<int> CreateAsync(int cinemaId, DateTime startTime, int movieId, int hallId, ProjectionType type);

        IEnumerable<IndexProjectionViewModel> AllProjectionsAdminArea();

    }
}