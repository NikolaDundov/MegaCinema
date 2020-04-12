namespace MegaCinema.Web.ViewModels.Projection
{
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class CinemaDropdownModel : IMapFrom<Cinema>
    {
        public int Id { get; set; }

        public string City { get; set; }
    }
}
