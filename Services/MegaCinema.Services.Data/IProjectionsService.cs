namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;

    using MegaCinema.Web.ViewModels.Projection;

    public interface IProjectionsService
    {
        IEnumerable<T> AllProjectionsByCinema<T>(string cinemaName);

        IEnumerable<T> AllProjections<T>();

        T ProjectionByProjectionId<T>(int? id);

        Task<T> ProjectionByProjectionIdAsync<T>(int? id);

        IEnumerable<T> ProjectionByMovieId<T>(int id);

        IEnumerable<T> ProjectionByMovieIdAdCinemaId<T>(int movieId, int cinemaId, DateTime startTime);

        IEnumerable<T> ProjectionByMovieIdAndCinemaIdOnly<T>(int movieId, int cinemaId);

        IEnumerable<T> ProjectionByCinemaIdAndDate<T>(int cinemaId, DateTime startTime);

        Task<int> CreateAsync(int cinemaId, DateTime startTime, int movieId, int hallId, ProjectionType type);

        IEnumerable<IndexProjectionViewModel> AllProjectionsAdminArea();

        Task UpdateProjection(ProjectionInputModel projection);

        bool ProjectionExists(int id);

        Task DeleteById(int id);

        Task DeleteByMovieId(int movieId);

        int ProjectionsCount();

        IEnumerable<DateTime> ProjectionsStartTime(int hallId);

        Task DeleteProjectionsRange(DateTime startDay, DateTime endDay);
    }
}
