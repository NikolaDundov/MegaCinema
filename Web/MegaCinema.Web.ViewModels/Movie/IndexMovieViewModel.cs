namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;

    public class IndexMovieViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Language Language { get; set; }

        public GenreType Genre { get; set; }

        public TimeSpan Duration { get; set; }

        public double Score { get; set; }

        public MPAARating Rating { get; set; }

        public string Director { get; set; }

        public Country Country { get; set; }
    }
}
