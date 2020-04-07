namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class FindProjectionInputModel : IMapTo<Projection>, IMapFrom<Projection>
    {
        public int Id { get; set; }

        [Display(Name = "Cinema")]
        public int CinemaId { get; set; }

        public IEnumerable<CinemaDropdownModel> Cinemas { get; set; }

        [Display(Name = "Movie")]
        public int? MovieId { get; set; }

        public IEnumerable<MovieDropdownModel> Movies { get; set; }

        [Display(Name = "Projection Date")]
        [DataType(DataType.Date)]
        public DateTime? StartTime { get; set; }
    }
}
