namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;

    public class MovieInputModel : IMapTo<Movie>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public GenreType Genre { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public string Trailer { get; set; }

        [Range(0, 10)]
        public double Score { get; set; }

        [Required]
        public MPAARating Rating { get; set; }

        [Required]
        public string Actors { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
