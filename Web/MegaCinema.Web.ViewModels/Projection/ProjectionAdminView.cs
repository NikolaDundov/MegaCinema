﻿namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class ProjectionAdminView : IMapFrom<Projection>
    {
        public int Id { get; set; }

        public string CinemaCity { get; set; }

        public DateTime StartTime { get; set; }

        public string MovieTitle { get; set; }

        public string HallName { get; set; }

        public ProjectionType Type { get; set; }
    }
}
