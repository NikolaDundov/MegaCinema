namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class CinemaService : ICinemaService
    {
        private readonly IRepository<Cinema> repository;

        public CinemaService(IRepository<Cinema> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> AllCinemas<T>()
        {
            IQueryable<Cinema> cinemas = this.repository.All();
            return cinemas.To<T>().ToList();
        }

        public IEnumerable<T> AllProjectionByDate<T>(DateTime date)
        {
            return null;
        }

        public T ShowProjections<T>(int id)
        {
            var cinema = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return cinema;
        }
    }
}
