namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.Infrastructure.CustomAttributes;

    public class ProjectionInputModel : IMapTo<Projection>, IMapFrom<Projection>
    {
        private const int MaxCinemaId = 1000000;

        public int Id { get; set; }

        [Display(Name = "Cinema")]
        [Required]
        public int CinemaId { get; set; }

        public IEnumerable<CinemaDropdownModel> Cinemas { get; set; }

        [Required]
        [Display(Name = "Start time")]
        [HoursValidation]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(1, MaxCinemaId)]
        [Display(Name = "Movie Title")]
        public int MovieId { get; set; }

        public IEnumerable<MovieDropdownModel> Movies { get; set; }

        [Required]
        [Display(Name = "Hall Name")]
        public int HallId { get; set; }

        public IEnumerable<HallDropdownModel> Halls { get; set; }

        [Required]
        [Display(Name = "Projectio Type")]
        public ProjectionType Type { get; set; }
    }
}
