namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.Projection;

    public class ProjectionsService : IProjectionsService
    {
        private readonly IRepository<Projection> repository;

        public ProjectionsService(IRepository<Projection> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> AllProjections<T>()
        {
            IQueryable<Projection> projections = this.repository.All();

            return projections.To<T>().ToList();
        }

        public IEnumerable<T> AllProjectionsByCinema<T>(string cinemaName)
        {
            IQueryable<Projection> projections = this.repository.All().Where(p => p.Cinema.City == cinemaName
            && p.StartTime.Day == DateTime.UtcNow.Day && p.StartTime.Month == DateTime.UtcNow.Month);

            return projections.To<T>().ToList();
        }

        public IEnumerable<ProjectionAdminView> GetAllProjections()
        {
            var projections = this.repository.All().ToList();
            var allProjectionsAdmin = new List<ProjectionAdminView>();

            foreach (var projection in projections)
            {
                var projectionView = new ProjectionAdminView
                {
                    Id = projection.Id,
                    CinemaCity = projection.Cinema.City,
                    HallName = projection.Hall.Name,
                    StartTime = projection.StartTime,
                    MovieTitle = projection.Movie.Title,
                    Type = projection.Type,
                };
                allProjectionsAdmin.Add(projectionView);
            }

            return allProjectionsAdmin;
        }

        public IEnumerable<T> ProjectionByMovieId<T>(int id)
        {
            var projections = this.repository.All().Where(x => x.MovieId == id
            && x.StartTime.Day >= DateTime.UtcNow.Day).To<T>().ToList();

            return projections;
        }

        public T ProjectionByProjectionId<T>(int id)
        {
            var projection = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return projection;
        }
    }
}