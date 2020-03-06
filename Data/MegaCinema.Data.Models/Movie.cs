namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Movie
    {
        public string Title { get; set; }

        public string Poster { get; set; }

        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        public double UsersRating { get; set; }

        public MPAARating Rating { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public string Director { get; set; }
    }
}
