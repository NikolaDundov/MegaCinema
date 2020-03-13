namespace MegaCinema.Web.ViewModels
{
    using System.Collections.Generic;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using MegaCinema.Services.Mapping;

    public class IndexMovieViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Language Language { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public string Poster { get; set; }

        public MPAARating Rating { get; set; }
    }
}
