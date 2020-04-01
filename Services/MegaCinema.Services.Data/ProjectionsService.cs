namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.EntityFrameworkCore;

    public class ProjectionsService : IProjectionsService
    {
        private readonly IRepository<Projection> projectionRepository;
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<Cinema> cinemaRepository;
        private readonly IRepository<Hall> hallRepository;
        private readonly IRepository<Seat> seatRepository;

        public ProjectionsService(
            IRepository<Projection> projectionRepository,
            IRepository<Movie> movieRepository,
            IRepository<Cinema> cinemaRepository,
            IRepository<Hall> hallRepository,
            IRepository<Seat> seatRepository)
        {
            this.projectionRepository = projectionRepository;
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.hallRepository = hallRepository;
            this.seatRepository = seatRepository;
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
                 .OrderByDescending(x => x.Id).ToList();

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
            var projection = this.projectionRepository.All()
                .Where(x => x.Id == id).To<T>().FirstOrDefault();

            return projection;
        }

        public async Task<int> CreateAsync(int cinemaId, DateTime startTime, int movieId, int hallId, ProjectionType type)
        {
            var projection = new Projection
            {
                CinemaId = cinemaId,
                HallId = hallId,
                MovieId = movieId,
                StartTime = startTime,
                Type = type,
                Seats = CreateSeats('L', 16),
            };

            await this.projectionRepository.AddAsync(projection);
            await this.projectionRepository.SaveChangesAsync();
            return projection.Id;
        }

        public async Task UpdateProjection(ProjectionInputModel projection)
        {
            var projectionToUpdate = AutoMapperConfig.MapperInstance.Map<Projection>(projection);
            this.projectionRepository.Update(projectionToUpdate);
            await this.movieRepository.SaveChangesAsync();
        }

        public bool ProjectionExists(int id)
        {
            return this.projectionRepository.All().Any(x => x.Id == id);
        }

        public async Task DeleteById(int id)
        {
            var seats = await this.seatRepository.All().Where(x => x.ProjectionId == id).ToListAsync();
            foreach (var seat in seats)
            {
                this.seatRepository.Delete(seat);
            }

            await this.seatRepository.SaveChangesAsync();
            var projection = await this.projectionRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.projectionRepository.Delete(projection);
        }

        private static List<Seat> CreateSeats(char lastRow, int firstRowSeatsCount)
        {
            var seats = new List<Seat>();
            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= firstRowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }
}
