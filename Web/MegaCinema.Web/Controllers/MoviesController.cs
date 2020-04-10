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

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.moviesService.GetByIdAsync<MovieViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult Available()
        {
            var viewModel = new AllMovieViewModel
            {
                AllMovies = this.moviesService.AllMovies<MovieViewModel>()
                .Where(x => x.ReleaseDate < DateTime.UtcNow)
                .ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Upcoming()
        {
            var viewModel = new AllMovieViewModel
            {
                AllMovies = this.moviesService.AllMovies<MovieViewModel>()
                .Where(x => x.ReleaseDate > DateTime.UtcNow)
                .ToList(),
            };

            return this.View(viewModel);
        }
    }
}
