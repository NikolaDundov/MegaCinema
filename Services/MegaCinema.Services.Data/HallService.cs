namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class HallService : IHallService
    {
        private readonly IRepository<Hall> hallRepository;

        public HallService(IRepository<Hall> hallRepository)
        {
            this.hallRepository = hallRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Hall> halls = this.hallRepository.All();
            return halls.To<T>().ToList();
        }
    }
}
