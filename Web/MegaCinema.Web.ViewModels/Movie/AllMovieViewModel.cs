namespace MegaCinema.Web.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllMovieViewModel
    {
        public ICollection<MovieViewModel> AllMovies { get; set; }
    }
}
