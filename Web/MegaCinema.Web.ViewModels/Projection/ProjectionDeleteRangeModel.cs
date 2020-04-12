namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ProjectionDeleteRangeModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From Day")]
        public DateTime StartDay { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Day")]
        public DateTime EndDay { get; set; }
    }
}
