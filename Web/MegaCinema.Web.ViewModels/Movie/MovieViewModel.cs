namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
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

        public List<string> Cast => this.Actors.Select(x => x.Name).ToList();

        public List<string> GenresToDisplay => this.Genres.Select(x => x.GenreType.ToString()).ToList();

        public string ReleaseDateToDisplay => this.ReleaseDate.ToString("MMMM dd yyyy", CultureInfo.CreateSpecificCulture("en-US"));

        public string Director { get; set; }

        public string Url => $"/Movies/{this.Id}";

        public virtual ICollection<Country> Countries { get; set; }

        public List<string> CoutriesToDisplay => this.Countries.Select(x => x.Name).ToList();

    }
}
