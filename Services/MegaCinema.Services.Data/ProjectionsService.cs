﻿namespace MegaCinema.Services.Data
{
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

        public IEnumerable<T> AllProjectionsByCinema<T>(int id)
        {
            IQueryable<Projection> projections = this.repository.All().Where(p => p.CinemaId == id 
            && p.StartTime.Day == DateTime.UtcNow.Day);

            return projections.To<T>().ToList();
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
