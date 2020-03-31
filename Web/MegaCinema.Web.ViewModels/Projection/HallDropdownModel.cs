namespace MegaCinema.Web.ViewModels.Projection
{
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class HallDropdownModel : IMapFrom<Hall>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}