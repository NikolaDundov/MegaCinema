namespace MegaCinema.Web.ViewModels.Projection
{
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class MovieDropdownModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}