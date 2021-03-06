﻿namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;
    using MegaCinema.Data.Models.Enums;

    public class Movie : BaseModel<int>
    {
        public Movie()
        {
            this.CinemaMovies = new HashSet<CinemaMovies>();
        }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public GenreType Genre { get; set; }

        public string Poster { get; set; }

        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        public double Score { get; set; }

        public MPAARating Rating { get; set; }

        public string Actors { get; set; }

        public string Director { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<CinemaMovies> CinemaMovies { get; set; }
    }
}
