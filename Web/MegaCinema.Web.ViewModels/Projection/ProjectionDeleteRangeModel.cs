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
        [Display(Name = "From Date")]
        public DateTime StartDay { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime EndDay { get; set; }
    }
}
