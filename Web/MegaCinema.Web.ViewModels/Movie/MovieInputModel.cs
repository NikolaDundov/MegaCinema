namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;

    public class MovieInputModel : IMapTo<Movie>, IMapFrom<Movie>
    {
        private const int MinimumTitleSymbols = 3;
        private const int MinimumDesriptionSymbols = 20;

        public int Id { get; set; }

        [Required]
        [MinLength(MinimumTitleSymbols)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MinLength(MinimumDesriptionSymbols)]
        public string Description { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public GenreType Genre { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string Trailer { get; set; }

        [Required]
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
