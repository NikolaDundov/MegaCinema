namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Cinema : BaseModel<int>
    {
        public Cinema()
        {
            this.Projections = new HashSet<Projection>();
            this.Halls = new HashSet<Hall>();
        }

        public string City { get; set; }

        public string Address { get; set; }

        public DateTime OpenHour { get; set; }

        public DateTime ClosingHour { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
    }
}
