namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.CustomAttributes;

    public class MovieInputModel : IMapTo<Movie>, IMapFrom<Movie>
    {
        private const int MinimumTitleSymbols = 3;
        private const int MinimumDesriptionSymbols = 20;
        private const int MinimumActorsSymbols = 15;
        private const int MinimumDirectorSymbols = 6;
        private const int MinimumScore = 1;
        private const int MaximumScore = 10;
        private const int MinLinkLength = 5;

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
        [MinLength(MinLinkLength)]
        public string Poster { get; set; }

        [Required]
        [MovieDuration]
        public TimeSpan Duration { get; set; }

        [Required]
        [MinLength(MinLinkLength)]
        public string Trailer { get; set; }

        [Required]
        [Range(MinimumScore, MaximumScore)]
        public double Score { get; set; }

        [Required]
        public MPAARating Rating { get; set; }

        [Required]
        [MinLength(MinimumActorsSymbols)]
        public string Actors { get; set; }

        [Required]
        [MinLength(MinimumDirectorSymbols)]
        public string Director { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
