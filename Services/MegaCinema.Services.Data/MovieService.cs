﻿namespace MegaCinema.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.Movie;

    public class MovieService : IMoviesService
    {
        private readonly IRepository<Movie> repository;

        public MovieService(IRepository<Movie> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> AllMovies<T>()
        {
            IQueryable<Movie> movies = this.repository.All();

            return movies.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var movie = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return movie;
        }
    }
}
