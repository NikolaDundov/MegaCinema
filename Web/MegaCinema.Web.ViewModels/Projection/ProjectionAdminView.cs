using MegaCinema.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MegaCinema.Web.ViewModels.Projection
{
    public class ProjectionAdminView
    {
        public int Id { get; set; }

        public string CinemaCity { get; set; }

        public DateTime StartTime { get; set; }

        public string MovieTitle { get; set; }

        public string HallName { get; set; }

        public ProjectionType Type { get; set; }
    }
}
