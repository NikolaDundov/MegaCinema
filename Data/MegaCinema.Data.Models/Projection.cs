using System;
using System.Collections.Generic;
using System.Text;

namespace MegaCinema.Data.Models
{
    public class Projection
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public int HallId { get; set; }

        public Hall Hall { get; set; }

        public ProjectioType Type { get; set; }
    }
}
