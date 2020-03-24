namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Linq;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class ProjectionInputModel : IMapTo<Projection>
    {
        public string CinemaCity { get; set; }

        public DateTime StartTime { get; set; }

        public string MovieTitle { get; set; }

        public string HallName { get; set; }

        public ProjectionType Type { get; set; }
    }
}
