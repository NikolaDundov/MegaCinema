namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class ProjectionViewModel : IMapFrom<Projection>
    {
        public int Id { get; set; }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public DateTime StartTime { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string MovieTitle { get; set; }

        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public ProjectionType Type { get; set; }

        public string projectionTypeString =>
            this.Type.ToString().Substring(1);

        public string LinkToMovie => $"/Movies/Details/{this.MovieId}";
    }
}
