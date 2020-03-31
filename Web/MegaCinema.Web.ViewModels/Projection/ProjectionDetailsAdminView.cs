namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class ProjectionDetailsAdminView : IMapFrom<Projection>
    {
        public int Id { get; set; }

        public virtual Cinema Cinema { get; set; }

        public DateTime StartTime { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Hall Hall { get; set; }

        public ProjectionType Type { get; set; }

        public string TypeStr => this.Type.ToString().Substring(1);
    }
}
