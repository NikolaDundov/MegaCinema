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

        public GenreType Genre { get; set; }

        public string Poster { get; set; }

        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        public double Score { get; set; }

        public MPAARating Rating { get; set; }

        public string Actors { get; set; }

        public string ReleaseDateToDisplay => this.ReleaseDate.ToString("MMMM dd yyyy", CultureInfo.CreateSpecificCulture("en-US"));

        public string Director { get; set; }

        public string Url => $"/Movies/Details/{this.Id}";

        public Country Country { get; set; }
    }
}
