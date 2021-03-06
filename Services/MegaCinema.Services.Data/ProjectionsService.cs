﻿namespace MegaCinema.Services.Data
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
        private const char LastRow = 'L';
        private const int LastSeat = 16;
        private readonly IRepository<Projection> projectionRepository;
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<Cinema> cinemaRepository;
        private readonly IRepository<Hall> hallRepository;
        private readonly IRepository<Seat> seatRepository;
        private readonly IRepository<Ticket> ticketRepository;

        public ProjectionsService(
            IRepository<Projection> projectionRepository,
            IRepository<Movie> movieRepository,
            IRepository<Cinema> cinemaRepository,
            IRepository<Hall> hallRepository,
            IRepository<Seat> seatRepository,
            IRepository<Ticket> ticketRepository)
        {
            this.projectionRepository = projectionRepository;
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.hallRepository = hallRepository;
            this.seatRepository = seatRepository;
            this.ticketRepository = ticketRepository;
        }

        public IEnumerable<T> AllProjections<T>()
        {
            IQueryable<Projection> projections = this.projectionRepository.All();

            return projections.To<T>().ToList();
        }

        public IEnumerable<T> AllProjectionsByCinema<T>(string cinemaName)
        {
            IQueryable<Projection> projections = this.projectionRepository
                .All().Where(p => p.Cinema.City == cinemaName
            && p.StartTime.Day == DateTime.UtcNow.Day
            && p.StartTime.Month == DateTime.UtcNow.Month);

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
            var projections = this.projectionRepository
                .All().Where(x => x.MovieId == id)
                .To<T>().ToList();

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
                Seats = CreateSeats(LastRow, LastSeat),
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
            var seats = await this.seatRepository.All()
                .Where(x => x.ProjectionId == id)
                .ToListAsync();

            foreach (var seat in seats)
            {
                this.seatRepository.Delete(seat);
            }

            await this.seatRepository.SaveChangesAsync();

            var tickets = this.ticketRepository.All()
                .Where(x => x.ProjectionId == id)
                .ToList();

            foreach (var ticket in tickets)
            {
                this.ticketRepository.Delete(ticket);
            }

            await this.ticketRepository.SaveChangesAsync();

            var projection = await this.projectionRepository
                .All().FirstOrDefaultAsync(x => x.Id == id);

            this.projectionRepository.Delete(projection);
            await this.projectionRepository.SaveChangesAsync();
        }

        public int ProjectionsCount()
        {
            return this.projectionRepository.All().Count();
        }

        public async Task DeleteByMovieId(int movieId)
        {
            var projections = await this.projectionRepository.All()
                .Where(x => x.MovieId == movieId).ToListAsync();

            if (projections == null)
            {
                return;
            }

            foreach (var projection in projections)
            {
                var seats = await this.seatRepository.All()
                    .Where(x => x.ProjectionId == projection.Id)
                    .ToListAsync();

                foreach (var seat in seats)
                {
                    this.seatRepository.Delete(seat);
                }

                var tickets = this.ticketRepository.All()
                        .Where(x => x.ProjectionId == projection.Id)
                        .ToList();

                foreach (var ticket in tickets)
                {
                    this.ticketRepository.Delete(ticket);
                }

                await this.ticketRepository.SaveChangesAsync();
                await this.seatRepository.SaveChangesAsync();
                this.projectionRepository.Delete(projection);
            }

            await this.projectionRepository.SaveChangesAsync();
        }

        public IEnumerable<DateTime> ProjectionsStartTime(int hallId)
        {
            var hallSchedule = this.projectionRepository.All()
                .Where(x => x.HallId == hallId)
                .Select(x => x.StartTime).ToList();

            return hallSchedule;
        }

        public IEnumerable<T> ProjectionByMovieIdAdCinemaId<T>(int movieId, int cinemaId, DateTime startTime)
        {
            var projections = this.projectionRepository.All()
                .Where(x => x.MovieId == movieId && x.CinemaId == cinemaId
                && x.StartTime.Date == startTime.Date).To<T>()
                .ToList();

            return projections;
        }

        public IEnumerable<T> ProjectionByCinemaIdAndDate<T>(int cinemaId, DateTime startTime)
        {
            var projections = this.projectionRepository.All()
                .Where(x => x.CinemaId == cinemaId
                && x.StartTime.Date == startTime.Date).To<T>()
                .ToList();

            return projections;
        }

        public IEnumerable<T> ProjectionByMovieIdAndCinemaIdOnly<T>(int movieId, int cinemaId)
        {
            var projections = this.projectionRepository.All()
                .Where(x => x.CinemaId == cinemaId
                            && x.MovieId == movieId
                            && x.StartTime.Date >= DateTime.UtcNow.Date)
                .OrderBy(x => x.StartTime)
                .To<T>()
                .ToList();

            return projections;
        }

        public async Task<T> ProjectionByProjectionIdAsync<T>(int? id)
        {
            var projection = await this.projectionRepository.All()
            .Where(x => x.Id == id)
            .To<T>().FirstOrDefaultAsync();

            return projection;
        }

        public async Task<int> DeleteProjectionsRange(DateTime startDay, DateTime endDay)
        {
            var projections = await this.projectionRepository
                .All().Where(x => x.StartTime.Date >= startDay.Date
                               && x.StartTime.Date <= endDay.Date)
                .ToListAsync();

            foreach (var projection in projections)
            {
                var seats = await this.seatRepository.All()
                .Where(x => x.ProjectionId == projection.Id)
                .ToListAsync();

                foreach (var seat in seats)
                {
                    this.seatRepository.Delete(seat);
                }

                await this.seatRepository.SaveChangesAsync();
                this.projectionRepository.Delete(projection);
                await this.projectionRepository.SaveChangesAsync();
            }

            await this.projectionRepository.SaveChangesAsync();
            return projections.Count;
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
