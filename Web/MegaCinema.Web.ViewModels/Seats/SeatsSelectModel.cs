namespace MegaCinema.Web.ViewModels.Seats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SeatsSelectModel
    {
        public SeatsSelectModel()
        {
            this.Rows = new HashSet<char>();
            this.Seats = new HashSet<int>();
        }

        public ICollection<char> Rows { get; set; }

        public ICollection<int> Seats { get; set; }
    }
}
