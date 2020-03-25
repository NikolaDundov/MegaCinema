namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TestProjectionView
    {
        public ICollection<string> CinemaNames { get; set; }

        public ICollection<string> HallsNames { get; set; }

        public ICollection<string> MovieNames { get; set; }
    }
}
