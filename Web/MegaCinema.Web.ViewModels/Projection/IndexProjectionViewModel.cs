namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MegaCinema.Data.Models;

    public class IndexProjectionViewModel
    {
        public int Id { get; set; }

        public string CinemaName { get; set; }

        public DateTime StartTime { get; set; }

        public string MovieTitle { get; set; }

        public string HallName { get; set; }

        public ProjectionType Type { get; set; }

        public string TypeStr => this.Type.ToString().Substring(1);
    }
}
