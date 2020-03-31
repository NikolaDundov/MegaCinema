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
        private readonly IRepository<Projection> projectionRepository;
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<Cinema> cinemaRepository;
        private readonly IRepository<Hall> hallRepository;

        public ProjectionsService(
            IRepository<Projection> projectionRepository,
            IRepository<Movie> movieRepository,
            IRepository<Cinema> cinemaRepository,
            IRepository<Hall> hallRepository)
        {
            this.projectionRepository = projectionRepository;
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.hallRepository = hallRepository;
        }

        public IEnumerable<T> AllProjections<T>()
        {
            IQueryable<Projection> projections = this.projectionRepository.All();

            return projections.To<T>().ToList();
        }

        public IEnumerable<T> AllProjectionsByCinema<T>(string cinemaName)
        {
            IQueryable<Projection> projections = this.projectionRepository.All().Where(p => p.Cinema.City == cinemaName
            && p.StartTime.Day == DateTime.UtcNow.Day && p.StartTime.Month == DateTime.UtcNow.Month);

            return projections.To<T>().ToList();
        }

        public IEnumerable<IndexProjectionViewModel> AllProjectionsAdminArea()
        {
            var projections = this
                 .projectionRepository.All()
                 .OrderByDescending(x => x.CreatedOn).ToList();

            var projectionsList = new List<IndexProjectionViewModel>();
            foreach (var projection in projections)
            {
                var movie = this.movieRepository.All().FirstOrDefault(x => x.Id == projection.MovieId);
                var hall = this.hallRepository.All().FirstOrDefault(x => x.Id == projection.HallId);
                var cinema = this.cinemaRepository.All().FirstOrDefault(x => x.Id == projection.CinemaId);

                var projectionToAdd = new IndexProjectionViewModel
                {
                    CinemaName = cinema.City,
                    HallName = hall.Name,
                    Id = projection.Id,
                    MovieTitle = movie.Title,
                    StartTime = projection.StartTime,
                    Type = projection.Type,
                };

                projectionsList.Add(projectionToAdd);
            }

            return projectionsList;
        }

        public IEnumerable<ProjectionAdminView> AllProjections()
        {
            var projections = this.projectionRepository.All().ToList();
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
            var projections = this.projectionRepository.All().Where(x => x.MovieId == id
            && x.StartTime.Day >= DateTime.UtcNow.Day).To<T>().ToList();

            return projections;
        }

        public T ProjectionByProjectionId<T>(int? id)
        {
            var projection = this.projectionRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return projection;
        }
    }
}