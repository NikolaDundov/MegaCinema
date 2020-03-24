namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;

    using MegaCinema.Data.Models;

    public class AllProjectionsAdminModel
    {
        public ICollection<ProjectionAdminView> Projections { get; set; }
    }
}
