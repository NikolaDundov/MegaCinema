namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;

    public class MovieViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public string Poster { get; set; }

        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        public double UsersRating { get; set; }

        public MPAARating Rating { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public string Director { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
