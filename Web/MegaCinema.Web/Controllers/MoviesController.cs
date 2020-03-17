namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.moviesService.GetById<MovieViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var viewModel = new AllMovieViewModel
            {
                AllMovies = this.moviesService.AllMovies<MovieViewModel>().ToList(),
            };

            return this.View(viewModel);
        }
    }
}
