namespace MegaCinema.Data.Models
{
    using System.Collections.Generic;

    using MegaCinema.Data.Common.Models;

    public class Hall : BaseModel<int>
    {
        //public Hall()
        //{
        //    this.Seats = new HashSet<Seat>();
        //}

        public string Name { get; set; }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        //public virtual ICollection<Seat> Seats { get; set; }
    }
}
