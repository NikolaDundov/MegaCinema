namespace MegaCinema.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexAllMoviesViewModel
    {
        public ICollection<IndexMovieViewModel> AllMovies { get; set; }
    }
}
