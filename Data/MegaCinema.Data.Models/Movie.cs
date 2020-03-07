namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Movie : BaseModel<int>
    {
        public Movie()
        {
            this.MovieActors = new HashSet<MovieActor>();
            this.Genres = new HashSet<Genre>();
            this.Projections = new HashSet<Projection>();
            this.Countries = new HashSet<Country>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public string Poster { get; set; }

        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        public double UsersRating { get; set; }

        public MPAARating Rating { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }

        public string Director { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
