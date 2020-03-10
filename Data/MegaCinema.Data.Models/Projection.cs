namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Projection : BaseModel<int>
    {
        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public DateTime StartTime { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public ProjectioType Type { get; set; }
    }
}
