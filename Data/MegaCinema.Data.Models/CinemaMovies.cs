namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CinemaMovies
    {
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
