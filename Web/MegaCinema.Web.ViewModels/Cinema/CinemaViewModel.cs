namespace MegaCinema.Web.ViewModels.Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class CinemaViewModel : IMapFrom<Cinema>
    {
        private DateTime currentDay = DateTime.UtcNow;

        public int Id { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public DateTime OpenHour { get; set; }

        public DateTime ClosingHour { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }

        public ICollection<Projection> ProjectionsForToday =>
            this.Projections.Where(x => x.StartTime == this.currentDay.AddDays(0)).ToList();

    }
}
