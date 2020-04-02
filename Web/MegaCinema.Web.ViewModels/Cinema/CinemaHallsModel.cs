namespace MegaCinema.Web.ViewModels.Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class CinemaHallsModel : IMapFrom<Cinema>
    {
        public CinemaHallsModel()
        {
            this.Halls = new HashSet<Hall>();
        }

        public int Id { get; set; }

        public ICollection<Hall> Halls { get; set; }
    }
}
