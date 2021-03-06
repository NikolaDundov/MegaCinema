﻿namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class CinemasService : ICinemaService
    {
        private readonly IRepository<Cinema> repository;

        public CinemasService(IRepository<Cinema> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> AllCinemas<T>()
        {
            IQueryable<Cinema> cinemas = this.repository.All();
            return cinemas.To<T>().ToList();
        }

        public T GetCinemaById<T>(int cinemaId)
        {
            var cinema = this.repository.All().Where(x => x.Id == cinemaId).To<T>().FirstOrDefault();
            return cinema;
        }

        public T ShowProjections<T>(int id)
        {
            var cinema = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return cinema;
        }
    }
}
